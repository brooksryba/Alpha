using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    public Vector3 position;

    public BattleSceneScriptable battleScriptable;
    
    Vector2 movement;

    void Start()
    {
        LoadState();
        transform.position = position;
    }
    
    public void SaveState()
    {
        SaveSystem.SaveState<PlayerLocationData>(new PlayerLocationData(this), "PlayerLocation");
    }

    public void LoadState()
    {
        PlayerLocationData data = SaveSystem.LoadState<PlayerLocationData>("PlayerLocation") as PlayerLocationData;
        if( data != null ) {
            this.position = new Vector3();
            this.position.x = data.position[0];
            this.position.y = data.position[1];
            this.position.z = 0;

        }
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy") {
            collision.gameObject.SetActive(false);
            collision.gameObject.GetComponent<Enemy>().SaveState();
            
            gameObject.GetComponent<Player>().SaveState();
            gameObject.GetComponent<PlayerMovement>().SaveState();
            
            battleScriptable.enemy = collision.gameObject.name;

            SceneManager.LoadScene(sceneName:"Battle");
        }

        if (collision.gameObject.tag == "InventoryItem") {
            InventoryItem item = collision.gameObject.GetComponent<InventoryItem>();

            gameObject.GetComponent<Player>().AddInventoryItem(item);
            gameObject.GetComponent<Player>().SaveState();
            
            collision.gameObject.SetActive(false);
            item.SaveState();
        }
    }    
}
