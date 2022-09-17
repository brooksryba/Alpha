using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    public bool isMainCharacter = false;
    public float baseMoveSpeed = 5f;
    public float moveSpeed;

    public Action targetCallback;
    public List<Vector3> targetLocations;
    public bool loadPosition = true; 

    public Rigidbody2D rb;
    public Animator animator;
    public Vector3 position;
    public LayerMask mask;

    private PlayerScriptable playerScriptable;

    private PlayerInteraction playerInteraction;
    
    Vector2 movement;

    void Start()
    {
        if(isMainCharacter) {
            playerScriptable = Resources.Load("Scriptable/PlayerScriptable") as PlayerScriptable;
            playerInteraction = gameObject.GetComponent<PlayerInteraction>();
            SaveSystem.instance.Register("PlayerLocation", () => { SaveState(); });
            if( loadPosition == true ) {
                LoadState();
                if(playerScriptable.ready) {
                    transform.position = playerScriptable.Read();
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
            SaveSystem.instance.SaveState<PlayerLocationData>(new PlayerLocationData(this), "PlayerLocation");
        }
    }

    public void LoadState()
    {
        if(isMainCharacter) {
            PlayerLocationData data = SaveSystem.instance.LoadState<PlayerLocationData>("PlayerLocation") as PlayerLocationData;
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


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)){
            moveSpeed = baseMoveSpeed * 2.0f;
        } else {
            moveSpeed = baseMoveSpeed; 
        }

        if(targetLocations.Count > 0) {
            position = targetLocations[0];
        }
        
        transform.position = Vector3.MoveTowards(transform.position, position, moveSpeed*Time.deltaTime);
        
        if(Vector3.Distance(transform.position, position) == 0) {
            if(targetLocations.Count > 0) { 
                targetLocations.RemoveAt(0);
                if(targetCallback != null && targetLocations.Count == 0)
                    targetCallback();
            } else if (isMainCharacter && playerInteraction != null && !playerInteraction.movementLock) {
                Vector3 newPosition = position;

                if(Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f) {
                    newPosition += new Vector3(Input.GetAxisRaw("Horizontal"), 0, 0);
                } else if(Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f) {
                    newPosition += new Vector3(0, Input.GetAxisRaw("Vertical"), 0);
                }

                Vector3 newPositionFeet = newPosition + new Vector3(0, -0.5f, 0); 
                if(!Physics2D.OverlapCircle(newPositionFeet, .25f, mask)) {
                    position = newPosition;
                }
            }
        }

        if( animator != null ) {
            Vector2 movement = new Vector2();
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.sqrMagnitude);
        }
    }
}
