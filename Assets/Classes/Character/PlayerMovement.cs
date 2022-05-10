using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{

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
        if( loadPosition == true) {
            LoadState();
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

    }

    void FixedUpdate()
    {
        if(!movementLock) {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Friendly" && !dialogLock) {
            dialogLock = true;

            Character friendly = collision.gameObject.GetComponent<Character>();
            HandleFriendly(collision);
            GameObject.Find("DialogSystem").GetComponent<DialogSystem>().Next(friendly, () => { dialogLock = false; }); 
        }

        if (collision.gameObject.tag == "Enemy" && !dialogLock) {
            dialogLock = true;
            movementLock = true;

            Character enemy = collision.gameObject.GetComponent<Character>();
            GameObject.Find("DialogSystem").GetComponent<DialogSystem>().Next(enemy, () => { dialogLock = false; HandleEnemy(collision); }); 
        }

        if (collision.gameObject.tag == "InventoryItem") {
            HandleInventoryItem(collision);
        }

        if (collision.gameObject.tag == "Portal"){
            HandlePortal(collision);
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
        collision.gameObject.SetActive(false);
        collision.gameObject.GetComponent<Character>().SaveState();
        collision.gameObject.GetComponent<Enemy>().SaveState();
        
        gameObject.GetComponent<Character>().SaveState();
        gameObject.GetComponent<Player>().SaveState();
        gameObject.GetComponent<PlayerMovement>().SaveState();
        
        battleScriptable.enemy = collision.gameObject.name;

        SceneManager.LoadScene(sceneName:"Battle");        
    }

    void HandleInventoryItem(Collision2D collision)
    {
        InventoryItem item = collision.gameObject.GetComponent<InventoryItem>();

        gameObject.GetComponent<Player>().AddInventoryItem(item);
        gameObject.GetComponent<Player>().SaveState();
        
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
