using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoonSharp.Interpreter;

public class CutsceneSystem : MonoBehaviour
{
    private static CutsceneSystem _instance;
    public static CutsceneSystem instance { get { return _instance; } }

    public BattleSceneScriptable battleScriptable;
    public PlayerScriptable playerScriptable;
    public bool cutsceneIsPlaying { get; private set; }
    public Dictionary<String, Vector3> originalPosition;

    private void Awake() { _instance = this; }

    public void EnterCutsceneMode()
    {
        cutsceneIsPlaying = true;
        originalPosition = new Dictionary<string, Vector3>();
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ExitCutsceneMode()
    {
        cutsceneIsPlaying = false;
        RestoreCharacterLocations();
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).gameObject.SetActive(false);
    }


    public void RestoreCharacterLocations()
    {
        foreach(string moverID in originalPosition.Keys) {
            GameObject character = GameObject.Find(moverID);
            character.transform.position = originalPosition[moverID];
        }
    }

    /**
     * Lua -- Cutscene Events
     */
    public void HandleCutsceneEvent(string tag) {
        Script script = new Script();
    
        //for each C# function we want to access from plaintext:
        script.Globals["Move"] = (Func<string, int, int, bool>)Move;
        script.Globals["Battle"] = (Func<string, string, bool>)Battle;
        script.Globals["Indicator"] = (Func<string, bool>)Indicator;
    
        //Once all functions have been registered
        script.DoString(tag);
    }    

    // PlayerMovement - this would need to apply to call animated chars
        // similar targetLocation to video except ours is a list
        // that could have more than one entry. when the destination
        // is reached the item is removed from the call stack 
        // FIFO (first in, first out)

    // Update Loop
        // if all events not done (each PlayerMovent.targetLocations.length != 0)
            // DialogSystem.instance.event_block = true
        // else
            // DialogSystem.instance.event_block = false
            

    //  MoveMultiple(moverID, List<int, int> moves)
        // call move once per item
        // create a call stack of moves for a character
        // dont remove this event until all moves completed
         
    private bool Move(string moverID, int x, int y)
    {

        // add event to event list


        GameObject character = GameObject.Find(moverID);
        if(!originalPosition.ContainsKey(moverID)) {
            originalPosition.Add(moverID, character.transform.position);
        }
        // set PlayerMovent.targetLocation to new position
        //character.transform.position = TileGrid.Translate(x, y);
        
        return true;
    }    

    private bool Battle(string enemyID, string storyPath)
    {
        battleScriptable.enemy = enemyID;
        battleScriptable.scene = SceneManager.GetActiveScene().name;
        battleScriptable.scenePath = storyPath;

        GameObject player = GameObject.Find("Player");
        playerScriptable.Write(player.transform.position);
        SaveSystem.instance.SaveAndDeregister();
        SceneManager.LoadScene(sceneName:"Battle");     

        return true;          
    }

    private bool Indicator(string charId)
    {
        transform.GetChild(1).gameObject.SetActive(true);

        GameObject obj = GameObject.Find(charId);
        GameObject indicator = GameObject.Find("CutsceneIndicator");

        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        indicator.transform.position = cam.WorldToScreenPoint(obj.transform.position);
        indicator.transform.position += new Vector3(0, 30, 0);
        return true;
    }
}
