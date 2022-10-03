using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoonSharp.Interpreter;
using Cinemachine;

public class CutsceneSystem : MonoBehaviour
{
    public static CutsceneSystem instance { get; private set; }
    private void OnEnable() { instance = this; }

    public static bool cutsceneIsPlaying { get; private set; }
    public static bool cutsceneInEvent { get; private set; }
    private List<Action> callbackEvents = new List<Action>();
    private Dictionary<String, Vector3> originalPosition;
    private List<String> spawnedCharacters = new List<String>();
    private GameObject indicatorTarget;
    public GameObject indicatorObject;
    public GameObject borderObject;
    public GameObject topBorderObject;
    public GameObject bottomBorderObject;

    private void Update() {
        if(cutsceneIsPlaying) {
            cutsceneInEvent = (callbackEvents.Count > 0);
        }
        if( indicatorTarget != null ) {
            Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
            indicatorObject.transform.position = cam.WorldToScreenPoint(indicatorTarget.transform.position);
            indicatorObject.transform.position += new Vector3(0, 30 * indicatorObject.transform.lossyScale.y, 0);
        }
    }

    public void EnterCutsceneMode()
    {    
        cutsceneIsPlaying = true;
        originalPosition = new Dictionary<string, Vector3>();
        borderObject.gameObject.SetActive(true);

        topBorderObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, 100f, 0f);
        LeanTween.moveY(topBorderObject.GetComponent<RectTransform>(), 50f, 1f);        

        bottomBorderObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, -100f, 0f);
        LeanTween.moveY(bottomBorderObject.GetComponent<RectTransform>(), -50f, 1f);             
    }

    public void ExitCutsceneMode()
    {
        LeanTween.moveY(topBorderObject.GetComponent<RectTransform>(), 100f, 1f).setOnComplete(() =>  borderObject.gameObject.SetActive(false));
        LeanTween.moveY(bottomBorderObject.GetComponent<RectTransform>(), -100f, 1f).setOnComplete(() =>  borderObject.gameObject.SetActive(false));

        indicatorObject.gameObject.SetActive(false);
        indicatorTarget = null;
        
        cutsceneIsPlaying = false;
        
        if( SceneManager.GetActiveScene().name == "Battle" )
            return;

        CinemachineVirtualCamera vcam = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = GameObject.Find("Player").transform;

        DestroySpawnedCharacters();
        RestoreCharacterLocations();
    }


    public void DestroySpawnedCharacters()
    {
        foreach(string charID in spawnedCharacters) {
            GameObject.Destroy(GameObject.Find(charID));
        }
        spawnedCharacters = new List<String>();
    }

    public void RestoreCharacterLocations()
    {
        foreach(string moverID in originalPosition.Keys) {
            GameObject character = GameObject.Find(moverID);
            if(character)
                character.transform.position = originalPosition[moverID];
        }
    }

    /**
     * Lua -- Cutscene Events
     */
    public void HandleCutsceneEvent(string tag) {
        Script script = new Script();
    
        //for each C# function we want to access from plaintext:
        script.Globals["Spawn"] = (Func<string, int, int, bool>)Spawn;
        script.Globals["Unspawn"] = (Func<string, bool>)Unspawn;
        script.Globals["Move"] = (Func<string, int, int, bool>)Move;
        script.Globals["MoveMultiple"] = (Func<string, List<List<int>>, bool>)MoveMultiple;
        script.Globals["Battle"] = (Func<string, string, bool>)Battle;
        script.Globals["Indicator"] = (Func<string, bool>)Indicator;
    
        //Once all functions have been registered
        script.DoString(tag); 
    }    

    private bool Spawn(string charID, int x, int y)
    {

        if(GameObject.Find(charID) != null)
            return true;
            
        GameObject characterList = GameObject.Find("Characters");
        GameObject characterRes = Resources.Load("Prefabs/Characters/"+charID) as GameObject;
        GameObject character = Instantiate(characterRes, characterList.transform);
        character.name = charID;
        character.transform.position = new Vector3(x, y, 0f);
        spawnedCharacters.Add(charID);
       
        return true;
    }

    private bool Unspawn(string charID)
    {
        if(GameObject.Find(charID) == null)
            return true;
        
        StartCoroutine(UnspawnHandler(charID));
        return true;
    }

    private IEnumerator UnspawnHandler(string charID)
    {
        while(callbackEvents.Count > 0) {
            yield return null;
        }

        GameObject.Find(charID).SetActive(false);
    }

    private void HandleEventCallback(Action self)
    {
        callbackEvents.Remove(self);
    }
    
    private bool Move(string moverID, int x, int y)
    {
        GameObject character = GameObject.Find(moverID);
        if(!originalPosition.ContainsKey(moverID) && moverID != "Player") {
            originalPosition.Add(moverID, character.transform.position);
        }
        
        Action cachedHandler = (() => {});
        cachedHandler = () => HandleEventCallback(cachedHandler);
        callbackEvents.Add(cachedHandler);

        CharacterMovement movement = character.GetComponent<CharacterMovement>();
        movement.targetCallback += cachedHandler;
        movement.targetLocations.Add(new Vector3(x, y, 0f));
       
        return true;
    }

    private bool MoveMultiple(string moverID, List<List<int>> xys){
        foreach(List<int> xy in xys) {
            Move(moverID, xy[0], xy[1]);
        }

        return true;
    }
 
    private bool Battle(string enemyID, string storyPath)
    {
        SceneSystem.battle = new BattleData(enemyID, SceneManager.GetActiveScene().name, storyPath);
        SceneSystem.world = new PlayerLocationData(GameObject.Find("Player").GetComponent<CharacterMovement>());
        
        SaveSystem.SaveAndDeregister();
        SceneManager.LoadScene(sceneName:"Battle");     

        StateSystem.instance.SetBool("worldInBattle", true);
        StateSystem.instance.Trigger("Battle");
        return true;          
    }

    private bool Indicator(string charId)
    {
        indicatorObject.gameObject.SetActive(true);
        indicatorTarget = GameObject.Find(charId);
                
        CinemachineVirtualCamera vcam = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = indicatorTarget.transform;

        return true;
    }
}
