using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float baseMoveSpeed = 5f;
    public float moveSpeed;

    public bool loadPosition = true; 

    public Rigidbody2D rb;
    public Animator animator;
    public Vector3 position;

    public PlayerScriptable playerScriptable;

    public PlayerInteraction playerInteraction;
    
    Vector2 movement;

    void Start()
    {
        SaveSystem.instance.Register("PlayerLocation", () => { SaveState(); });
        if( loadPosition == true ) {
            LoadState();
            if(playerScriptable.ready) {
                transform.position = playerScriptable.Read();
            } else {
                if(!(position.x == 0 && position.y == 0))
                    transform.position = position;
            }
        }
    }
    
    public void SaveState()
    {
        SaveSystem.instance.SaveState<PlayerLocationData>(new PlayerLocationData(this), "PlayerLocation");
    }

    public void LoadState()
    {
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


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)){
            moveSpeed = baseMoveSpeed * 2.0f;
        } else {
            moveSpeed = baseMoveSpeed;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        Vector2 clamp = new Vector2(
            Mathf.RoundToInt(transform.position.x * 16), 
        Mathf.RoundToInt(transform.position.y * 16)) / 16;

        transform.position = clamp;
    }

    void FixedUpdate()
    {
        if(!playerInteraction.movementLock) {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
