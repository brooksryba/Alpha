using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Collision2D collision;
    public float moveSpeed = 5f;

    public bool loadPosition = true; 

    public Rigidbody2D rb;
    public Animator animator;
    public Vector3 position;

    public BattleSceneScriptable battleScriptable;
    
    Vector2 movement;
    public bool movementLock = false;
    public bool dialogLock = false;

    void Start()
    {
        if( loadPosition == true ) {
            LoadState();
            if(!(position.x == 0 && position.y == 0))
                transform.position = position;
        }
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

        Vector2 clamp = new Vector2(
            Mathf.RoundToInt(transform.position.x * 16), 
        Mathf.RoundToInt(transform.position.y * 16)) / 16;

        transform.position = clamp;
    }

    void FixedUpdate()
    {
        if(!movementLock) {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }

        Character player = gameObject.GetComponent<Character>();
        if(player.currentHP == 0) {
            player.currentHP = (int)(player.maxHP / 2);
            player.SaveState();

            this.position = new Vector3();
            this.position.x = 0;
            this.position.y = 0;
            this.position.z = 0;
            transform.position = this.position;
            SaveState();

            GameObject.Find("ToastSystem").GetComponent<ToastSystem>().Open("You have fainted. Your health was reset to 25%");
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        collision = other;
        HandleCollisionStatic();
    }

    void OnCollisionExit2D(Collision2D other) {
        collision = null;
    } 

    public void HandleCollisionStatic()
    {
        if(collision != null) {
            if (collision.gameObject.tag == "InventoryItem") {
                HandleInventoryItem(collision);
            } else if (collision.gameObject.tag == "Portal"){
                HandlePortal(collision);
            }
        }
    }           

    public void HandleCollisionInteraction()
    {
        if(collision != null) {
            if (collision.gameObject.tag == "Friendly" && !dialogLock) {
                dialogLock = true;

                Character friendly = collision.gameObject.GetComponent<Character>();
                HandleFriendly(collision);
                GameObject.Find("DialogSystem").GetComponent<DialogSystem>().Next(friendly, () => { dialogLock = false; }); 
            } else if (collision.gameObject.tag == "Enemy" && !dialogLock) {
                dialogLock = true;
                movementLock = true;

                Character enemy = collision.gameObject.GetComponent<Character>();
                GameObject.Find("DialogSystem").GetComponent<DialogSystem>().Next(enemy, () => { dialogLock = false; movementLock = false; HandleEnemy(collision); }); 
            }
        }
    }    

    void HandleFriendly(Collision2D collision)
    {
        Character player = gameObject.GetComponent<Character>();
        Character friendly = collision.gameObject.GetComponent<Character>();
        
        if(friendly.dialogIndex == 1) {
            if(!player.partyMembers.Contains("Characters/"+collision.gameObject.name)) {
                player.partyMembers.Add("Characters/"+collision.gameObject.name);
                gameObject.GetComponent<Character>().SaveState();

                GameObject.Find("ToastSystem").GetComponent<ToastSystem>().Open(friendly.title + " joined your party!");
            }        
        }
    }

    void HandleEnemy(Collision2D collision)
    {
        gameObject.GetComponent<PlayerMovement>().SaveState();

        Character enemy = collision.gameObject.GetComponent<Character>();
        if(enemy.currentHP > 0) {
            battleScriptable.enemy = collision.gameObject.name;
            SceneManager.LoadScene(sceneName:"Battle");        
        } else {
            collision.gameObject.SetActive(false);
            collision.gameObject.GetComponent<Enemy>().SaveState();
        }
    }

    void HandleInventoryItem(Collision2D collision)
    {
        InventoryItem item = collision.gameObject.GetComponent<InventoryItem>();

        gameObject.GetComponent<Character>().AddInventoryItem(item);
        gameObject.GetComponent<Character>().SaveState();
        
        collision.gameObject.SetActive(false);
        item.SaveState();

        string itemName = collision.gameObject.name.ToLower();
        string message = "Picked up a";
        if("aeiou".Contains(itemName[0].ToString())) {
            message += "n";
        }
        message += " " + itemName + ".";

        GameObject.Find("ToastSystem").GetComponent<ToastSystem>().Open(message);        
    }

    void HandlePortal(Collision2D collision)
    {
        if( loadPosition == true ) {
            gameObject.transform.position -= new Vector3(0, 1, 0);
            SaveState();  
        } 
        string gname = collision.gameObject.name.Substring(7);
        SceneManager.LoadScene(sceneName: gname);        
    }     
}
