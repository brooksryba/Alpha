using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;

public class CutsceneSystem : MonoBehaviour
{
    public bool cutsceneIsPlaying { get; private set; }
    public Dictionary<String, Vector3> originalPosition;

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
        script.Globals["Move"] = (Func<string, float, float, bool>)Move;
    
        //Once all functions have been registered
        script.DoString(tag);
    }    

    private bool Move(string moverID, float x, float y)
    {
        GameObject character = GameObject.Find(moverID);
        if(!originalPosition.ContainsKey(moverID)) {
            originalPosition.Add(moverID, character.transform.position);
        }
        character.transform.position = new Vector3(x, y, 0);
        return true;
    }    
}
