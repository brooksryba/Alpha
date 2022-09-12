using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Collision2D collision;
    public GameObject collisionObject;
    public Character collisionCharacter;
    public Cutscene collisionCutscene;
    public float baseMoveSpeed = 5f;
    public float moveSpeed;

    public bool loadPosition = true; 

    public Rigidbody2D rb;
    public Animator animator;
    public Vector3 position;

    public BattleSceneScriptable battleScriptable;
    public PlayerScriptable playerScriptable;
    
    Vector2 movement;
    public bool movementLock = false;
    public bool dialogLock = false;
    public bool cutsceneLock = false;

    void Start()
    {
        SaveSystem.Register("PlayerLocation", () => { SaveState(); });
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
        SaveSystem.SaveState<PlayerLocationData>(new PlayerLocationData(this), "PlayerLocation");
    }

    public void LoadState()
    {
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
        if(!movementLock) {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
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
            } else if (collision.gameObject.tag == "Cutscene") {
                HandleCutscene(collision);
            }
        }
    }           

    public void HandleCollisionInteraction()
    {
        CutsceneSystem cutsceneSystem = GameObject.Find("CutsceneSystem").GetComponent<CutsceneSystem>();
        DialogSystem dialogSystem = GameObject.Find("DialogSystem").GetComponent<DialogSystem>();

        if(cutsceneSystem.cutsceneIsPlaying) {
            cutsceneLock = true;
            movementLock = true;
            dialogSystem.ContinueStory();
        } else if (dialogSystem.dialogueIsPlaying) {
            dialogLock = true;
            movementLock = true;
            dialogSystem.ContinueStory();
        } else {
            if(collision != null){
                if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Friendly") && !dialogLock) {
                    collisionObject = collision.gameObject;
                    collisionCharacter = collision.gameObject.GetComponent<Character>();
                    if(collisionCharacter.inkJSON) {
                        dialogLock = true;
                        movementLock = true;
                        dialogSystem.EnterDialogueMode(collisionCharacter.inkJSON, (s) => {HandleDialogEvent(s);}, () => {HandleDialogEnd();});
                        dialogSystem.ContinueStory();
                    }
                } else if(collision.gameObject.tag == "Cuttake" && !cutsceneLock) {
                    collisionObject = collision.gameObject;
                    collisionCutscene = collision.gameObject.GetComponent<Cutscene>();
                    if(collisionCutscene.inkJSON) {
                        cutsceneLock = true;
                        movementLock = true;
                        cutsceneSystem.EnterCutsceneMode();
                        dialogSystem.EnterDialogueMode(collisionCutscene.inkJSON, (s) => {HandleCutsceneEvent(s);}, () => {HandleCuttakeEnd();});
                        dialogSystem.ContinueStory();
                    }                    
                }
            }
        }
    }

    void HandleDialogEvent(string tag) {
        if(tag == "join_party") {
            HandleFriendly();
        } else if(tag == "battle") {
            HandleEnemy();
        } else {
            Debug.Log("Unhandled Tag: " + tag);
        }
    }

    void HandleDialogEnd() {
        dialogLock = false;
        movementLock = false;
    }

    void HandleFriendly()
    {
        Character player = gameObject.GetComponent<Character>();
        
        if(!player.partyMembers.Contains("Characters/"+collisionObject.name)) {
            player.partyMembers.Add("Characters/"+collisionObject.name);
            GameObject.Find("ToastSystem").GetComponent<ToastSystem>().Open(collisionCharacter.title + " joined your party!");
        }        
    }

    void HandleEnemy()
    {
        battleScriptable.enemy = collisionObject.name;
        battleScriptable.scene = SceneManager.GetActiveScene().name;
        playerScriptable.Write(transform.position);
        SaveSystem.SaveAndDeregister();
        SceneManager.LoadScene(sceneName:"Battle");        
    }

    void HandleInventoryItem(Collision2D collision)
    {
        InventoryItem item = collision.gameObject.GetComponent<InventoryItem>();

        gameObject.GetComponent<Character>().AddInventoryItem(item);
        
        collision.gameObject.SetActive(false);

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
        Portal portal = collision.gameObject.GetComponent<Portal>();
        playerScriptable.Write(portal.target);
        SaveSystem.SaveAndDeregister();
        SceneManager.LoadScene(sceneName: portal.scene);        
    }     

    void HandleCutscene(Collision2D collision)
    {
        CutsceneSystem cutsceneSystem = GameObject.Find("CutsceneSystem").GetComponent<CutsceneSystem>();
        DialogSystem dialogSystem = GameObject.Find("DialogSystem").GetComponent<DialogSystem>();

        if(collision != null){
            if ((collision.gameObject.tag == "Cutscene") && !cutsceneLock) {
                collisionObject = collision.gameObject;
                collisionCutscene = collision.gameObject.GetComponent<Cutscene>();

                if(collisionCutscene.inkJSON) {
                    cutsceneLock = true;
                    movementLock = true;
                    cutsceneSystem.EnterCutsceneMode();
                    dialogSystem.EnterDialogueMode(collisionCutscene.inkJSON, (s) => {HandleCutsceneEvent(s);}, () => {HandleCutsceneEnd();});
                    if(battleScriptable.scenePath != "") {
                        dialogSystem.SetStoryCurrentPath(battleScriptable.scenePath);
                        battleScriptable.scenePath = null;
                    }
                    dialogSystem.ContinueStory();
                }
            }
        }        
    }
    void HandleCutsceneEvent(string tag) {
        CutsceneSystem cutsceneSystem = GameObject.Find("CutsceneSystem").GetComponent<CutsceneSystem>();
        cutsceneSystem.HandleCutsceneEvent(tag);
    }

    void HandleCutsceneEnd() {
        CutsceneSystem cutsceneSystem = GameObject.Find("CutsceneSystem").GetComponent<CutsceneSystem>();
        cutsceneSystem.ExitCutsceneMode();
        collisionObject.SetActive(false);

        cutsceneLock = false;
        movementLock = false;
    }

    void HandleCuttakeEnd() {
        CutsceneSystem cutsceneSystem = GameObject.Find("CutsceneSystem").GetComponent<CutsceneSystem>();
        cutsceneSystem.ExitCutsceneMode();

        cutsceneLock = false;
        movementLock = false;
    }    

}
