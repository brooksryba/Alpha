using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    private GameObject collisionObject;
    private Character collisionCharacter;
    private Cutscene collisionCutscene;

    public BattleSceneScriptable battleScriptable;
    public PlayerScriptable playerScriptable;
    
    public bool movementLock = false;
    public bool dialogLock = false;
    public bool cutsceneLock = false;

    void OnCollisionEnter2D(Collision2D other) {
        collisionObject = other.gameObject;
        HandleCollisionStatic();
    }

    void OnCollisionExit2D(Collision2D other) {
        collisionObject = null;
    }

    void OnTriggerEnter2D(Collider2D other) {
        collisionObject = other.gameObject;
        HandleCollisionStatic();
    } 

    void OnTriggerExit2D(Collider2D other) {
        collisionObject = null;
    }

    public void HandleCollisionStatic()
    {
        if(collisionObject != null) {
            if (collisionObject.tag == "InventoryItem") {
                HandleInventoryItem();
            } else if (collisionObject.tag == "Portal"){
                HandlePortal();
            } else if (collisionObject.tag == "Cutscene" || collisionObject.tag == "Block") {
                HandleCutscene();
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
            if(collisionObject != null){
                if ((collisionObject.tag == "Enemy" || collisionObject.tag == "Friendly") && !dialogLock) {
                    collisionCharacter = collisionObject.GetComponent<Character>();
                    if(collisionCharacter.inkJSON) {
                        dialogLock = true;
                        movementLock = true;
                        DialogSystem.instance.EnterDialogueMode(collisionCharacter.inkJSON, (s) => {HandleDialogEvent(s);}, () => {HandleDialogEnd();});
                        DialogSystem.instance.ContinueStory();
                    }
                } else if(collisionObject.tag == "Cuttake" && !cutsceneLock) {
                    collisionCutscene = collisionObject.GetComponent<Cutscene>();
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
        SaveSystem.instance.SaveAndDeregister();
        SceneManager.LoadScene(sceneName:"Battle");        
    }

    void HandleInventoryItem()
    {
        InventoryItem item = collisionObject.GetComponent<InventoryItem>();

        gameObject.GetComponent<Character>().AddInventoryItem(item);
        
        collisionObject.SetActive(false);

        string itemName = collisionObject.name.ToLower();
        string message = "Picked up a";
        if("aeiou".Contains(itemName[0].ToString())) {
            message += "n";
        }
        message += " " + itemName + ".";

        ToastSystem.instance.Open(message);        
    }

    void HandlePortal()
    {
        Portal portal = collisionObject.GetComponent<Portal>();
        playerScriptable.Write(portal.target);
        SaveSystem.instance.SaveAndDeregister();
        SceneManager.LoadScene(sceneName: portal.scene);        
    }     

    void HandleCutscene()
    {
        if(!cutsceneLock){
            if (collisionObject.tag == "Cutscene") {
                collisionCutscene = collisionObject.GetComponent<Cutscene>();

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
            } else if(collisionObject.tag == "Block") {
                collisionCutscene = collisionObject.GetComponent<Cutscene>();

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
