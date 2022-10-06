using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    private GameObject collisionObject;
    private Character collisionCharacter;
    private Cutscene collisionCutscene;
    private Cuttake collisionCuttake;

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
        if(CutsceneSystem.cutsceneIsPlaying) {
            DialogSystem.instance.ContinueStory();
        } else if (DialogSystem.dialogueIsPlaying) {
            DialogSystem.instance.ContinueStory();
        } else {
            if(collisionObject != null){
                if ((collisionObject.tag == "Enemy" || collisionObject.tag == "Friendly")) {
                    HandleDialog();
                } else if(collisionObject.tag == "Cuttake") {
                    HandleCuttake();
                }
            }
        }
    }

    void HandleInventoryItem()
    {
        Item item = collisionObject.GetComponent<Item>();
        Character character = CharacterManager.Get(0); // CharacterManager.refs[0];
        // TODO - conditions will be changing data structure of items and will also be storing references to all characters in character manager
        character.condition.items.Add((item, 1));
        //gameObject.GetComponent<Character>().AddInventoryItem(item);
        string itemName = collisionObject.name.ToLower();
        collisionObject.SetActive(false);

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
        SceneSystem.world = new PlayerLocationData(portal.target, null);
        SaveSystem.SaveAndDeregister();
        SceneManager.LoadScene(sceneName: portal.scene);        
    }     

    void HandleDialog()
    {
        collisionCharacter = collisionObject.GetComponent<Character>();
        // TODO - This logic should be handled should call out to the story system to see if there an applicable resource
        if(collisionCharacter.inkJSON) {
            DialogSystem.instance.EnterDialogueMode(collisionCharacter.inkJSON, (s) => {}, () => {});
            DialogSystem.instance.ContinueStory();
        }
    }

    void HandleCutscene(bool advanceStory = true)
    {
        collisionCutscene = collisionObject.GetComponent<Cutscene>();

        StateSystem.instance.SetInteger("cutsceneChapter", collisionCutscene.chapter);
        StateSystem.instance.SetInteger("cutsceneMark", collisionCutscene.mark);
        StateSystem.instance.Trigger("Cutscene");
    }

    void HandleCuttake()
    {
        collisionCuttake = collisionObject.GetComponent<Cuttake>();

        StateSystem.instance.SetInteger("cuttakeID", collisionCuttake.id);
        StateSystem.instance.Trigger("Cuttake");        
    }
}
