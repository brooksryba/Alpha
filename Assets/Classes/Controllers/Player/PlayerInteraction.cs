using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    private Collision2D collision;
    private GameObject collisionObject;
    private Character collisionCharacter;
    private Cutscene collisionCutscene;

    public BattleSceneScriptable battleScriptable;
    public PlayerScriptable playerScriptable;
    
    public bool movementLock = false;
    public bool dialogLock = false;
    public bool cutsceneLock = false;

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
            } else if (collision.gameObject.tag == "Cutscene" || collision.gameObject.tag == "Block") {
                HandleCutscene(collision);
            }
        }
    }           

    public void HandleCollisionInteraction()
    {
        if(CutsceneSystem.instance.cutsceneIsPlaying) {
            cutsceneLock = true;
            movementLock = true;
            DialogSystem.instance.ContinueStory();
        } else if (DialogSystem.instance.dialogueIsPlaying) {
            dialogLock = true;
            movementLock = true;
            DialogSystem.instance.ContinueStory();
        } else {
            if(collision != null){
                if ((collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Friendly") && !dialogLock) {
                    collisionObject = collision.gameObject;
                    collisionCharacter = collision.gameObject.GetComponent<Character>();
                    if(collisionCharacter.inkJSON) {
                        dialogLock = true;
                        movementLock = true;
                        DialogSystem.instance.EnterDialogueMode(collisionCharacter.inkJSON, (s) => {HandleDialogEvent(s);}, () => {HandleDialogEnd();});
                        DialogSystem.instance.ContinueStory();
                    }
                } else if(collision.gameObject.tag == "Cuttake" && !cutsceneLock) {
                    collisionObject = collision.gameObject;
                    collisionCutscene = collision.gameObject.GetComponent<Cutscene>();
                    if(collisionCutscene.inkJSON) {
                        cutsceneLock = true;
                        movementLock = true;
                        CutsceneSystem.instance.EnterCutsceneMode();
                        DialogSystem.instance.EnterDialogueMode(collisionCutscene.inkJSON, (s) => {HandleCutsceneEvent(s);}, () => {HandleCuttakeEnd();});
                        DialogSystem.instance.ContinueStory();
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
            ToastSystem.instance.Open(collisionCharacter.title + " joined your party!");
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

        ToastSystem.instance.Open(message);        
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
        if(collision != null && !cutsceneLock){
            if (collision.gameObject.tag == "Cutscene") {
                collisionObject = collision.gameObject;
                collisionCutscene = collision.gameObject.GetComponent<Cutscene>();

                if(collisionCutscene != null && collisionCutscene.inkJSON) {
                    cutsceneLock = true;
                    movementLock = true;
                    CutsceneSystem.instance.EnterCutsceneMode();
                    DialogSystem.instance.EnterDialogueMode(collisionCutscene.inkJSON, (s) => {HandleCutsceneEvent(s);}, () => {HandleCutsceneEnd();});
                    if(battleScriptable.scenePath != null && battleScriptable.scenePath != "") {
                        DialogSystem.instance.SetStoryCurrentPath(battleScriptable.scenePath);
                        battleScriptable.scenePath = null;
                    }
                    DialogSystem.instance.ContinueStory();
                }
            } else if(collision.gameObject.tag == "Block") {
                collisionObject = collision.gameObject;
                collisionCutscene = collision.gameObject.GetComponent<Cutscene>();

                if(collisionCutscene != null && collisionCutscene.inkJSON) {
                    cutsceneLock = true;
                    movementLock = true;
                    CutsceneSystem.instance.EnterCutsceneMode();
                    DialogSystem.instance.EnterDialogueMode(collisionCutscene.inkJSON, (s) => {HandleCutsceneEvent(s);}, () => {HandleCuttakeEnd();});
                    DialogSystem.instance.ContinueStory();
                }                     
            }
        }        
    }
    void HandleCutsceneEvent(string tag) {
        CutsceneSystem.instance.HandleCutsceneEvent(tag);
    }

    void HandleCutsceneEnd() {
        CutsceneSystem.instance.ExitCutsceneMode();
        StorySystem.instance.MoveToNextMark();
        
        cutsceneLock = false;
        movementLock = false;
    }

    void HandleCuttakeEnd() {
        CutsceneSystem.instance.ExitCutsceneMode();

        cutsceneLock = false;
        movementLock = false;
    }    

}
