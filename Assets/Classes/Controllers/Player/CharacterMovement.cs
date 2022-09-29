using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    public bool isMainCharacter = false;
    public bool isAnimating = false;
    public float baseMoveSpeed = 5f;
    public float moveSpeed;

    public Vector2 targetVector = new Vector2();
    public Action targetCallback;
    public List<Vector3> targetLocations = new List<Vector3>();
    public bool loadPosition = true; 

    public Rigidbody2D rb;
    public Animator animator;
    public Vector3 position;
    public LayerMask mask;

    private PlayerInteraction playerInteraction;
    
    public bool isSprinting = false;
    Vector2 movement;

    void Start()
    {
        if(isMainCharacter && SceneManager.GetActiveScene().name != "Battle") {
            playerInteraction = gameObject.GetComponent<PlayerInteraction>();
            SaveSystem.Register("PlayerLocation", () => { SaveState(); });
            if( loadPosition == true ) {
                LoadState();
                if(SceneSystem.world != null) {
                    transform.position = new Vector3(SceneSystem.world.position[0], SceneSystem.world.position[1], 0f);
                    position = transform.position;

                    SceneSystem.world = null;
                } else {
                    if(!(position.x == 0 && position.y == 0)) {
                        transform.position = position;
                    } else {
                        position = transform.position;
                    }
                }
            }
        } else {
            position = transform.position;
        }
    }
    
    public void SaveState()
    {
        if(isMainCharacter) {
            SaveSystem.SaveState<PlayerLocationData>(new PlayerLocationData(this), "PlayerLocation");
        }
    }

    public void LoadState()
    {
        if(isMainCharacter) {
            PlayerLocationData data = SaveSystem.LoadState<PlayerLocationData>("PlayerLocation") as PlayerLocationData;
            if( data != null ) {
                if(data.scene == SceneManager.GetActiveScene().name) {
                    this.position = new Vector3();
                    this.position.x = data.position[0];
                    this.position.y = data.position[1];
                    this.position.z = 0;
                }
            }
        }
    }

    public void OnMove(InputValue input)
    {
        targetVector = input.Get<Vector2>();
    }

    public void OnSprint(InputValue input)
    {
        isSprinting = !isSprinting;
    }

    void Update()
    {
        if (isSprinting){
            moveSpeed = baseMoveSpeed * 2.0f;
        } else {
            moveSpeed = baseMoveSpeed; 
        }

        if(targetLocations.Count > 0) {
            isAnimating = true;
            position = targetLocations[0];
        } else {
            isAnimating = false;
        }
        
        int componentX = (Mathf.Abs(transform.position.x - position.x) < 0.02f ? 0 : (transform.position.x > position.x ? -1 : 1));
        int componentY = (Mathf.Abs(transform.position.y - position.y) < 0.02f ? 0 : (transform.position.y > position.y ? -1 : 1));
        transform.position = Vector3.MoveTowards(transform.position, position, moveSpeed*Time.deltaTime);

        if(Vector3.Distance(transform.position, position) <= 0.01f) {
            if(targetLocations.Count > 0) { 
                targetLocations.RemoveAt(0);
                if(targetCallback != null && targetLocations.Count == 0)
                    targetCallback();
            } else if (isMainCharacter && playerInteraction != null) {
                Vector3 newPosition = position;

                if(Mathf.Abs(targetVector.x) == 1f) {
                    newPosition += new Vector3(targetVector.x, 0, 0);
                } else if(Mathf.Abs(targetVector.y) == 1f) {
                    newPosition += new Vector3(0, targetVector.y, 0);
                }

                Vector3 newPositionFeet = newPosition + new Vector3(0, -0.5f, 0); 
                if(!Physics2D.OverlapBox(newPositionFeet, new Vector2(.5f, .5f), 0, mask)) {
                    position = newPosition;
                }
            }
        }

        if( animator != null ) {
            Vector2 movement = new Vector2();
            movement.x = componentX;
            movement.y = componentY;
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude * (moveSpeed / baseMoveSpeed));
        }
    }
}
