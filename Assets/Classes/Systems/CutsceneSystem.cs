using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MoonSharp.Interpreter;
using Cinemachine;

public class CutsceneSystem : MonoBehaviour
{
    private static CutsceneSystem _instance;
    public static CutsceneSystem instance { get { return _instance; } }

    public BattleSceneScriptable battleScriptable;
    public PlayerScriptable playerScriptable;
    public bool cutsceneIsPlaying { get; private set; }
    public bool cutsceneInEvent { get; private set; }
    private List<Action> callbackEvents = new List<Action>();
    private Dictionary<String, Vector3> originalPosition;
    private List<String> spawnedCharacters = new List<String>();
    private GameObject indicatorTarget;
    public GameObject indicatorObject;
    public GameObject borderObject;
    public GameObject topBorderObject;
    public GameObject bottomBorderObject;


    private void Awake() { _instance = this; }

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
        CinemachineVirtualCamera vcam = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = GameObject.Find("Player").transform;

        cutsceneIsPlaying = false;
        DestroySpawnedCharacters();
        RestoreCharacterLocations();

        LeanTween.moveY(topBorderObject.GetComponent<RectTransform>(), 100f, 1f).setOnComplete(() =>  borderObject.gameObject.SetActive(false));
        LeanTween.moveY(bottomBorderObject.GetComponent<RectTransform>(), -100f, 1f).setOnComplete(() =>  borderObject.gameObject.SetActive(false));

        indicatorObject.gameObject.SetActive(false);
        indicatorTarget = null;
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
        character.transform.position = TileGrid.Translate(x, y);
        spawnedCharacters.Add(charID);
       
        return true;
    }

    private void HandleEventCallback(Action self)
    {
        callbackEvents.Remove(self);
    }
    
    private bool Move(string moverID, int x, int y)
    {
        GameObject character = GameObject.Find(moverID);
        if(!originalPosition.ContainsKey(moverID)) {
            originalPosition.Add(moverID, character.transform.position);
        }
        
        Action cachedHandler = (() => {});
        cachedHandler = () => HandleEventCallback(cachedHandler);
        callbackEvents.Add(cachedHandler);

        CharacterMovement movement = character.GetComponent<CharacterMovement>();
        movement.targetCallback += cachedHandler;
        movement.targetLocations.Add(TileGrid.Translate(x, y));
       
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
        indicatorObject.gameObject.SetActive(true);
        indicatorTarget = GameObject.Find(charId);
                
        CinemachineVirtualCamera vcam = GameObject.Find("Virtual Camera").GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = indicatorTarget.transform;

        return true;
    }
}
