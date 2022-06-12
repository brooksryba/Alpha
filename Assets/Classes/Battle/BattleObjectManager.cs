using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleObjectManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI turnCounterText;
    
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public GameObject playerBattleStation;
    public GameObject enemyBattleStation;

    public GameObject playerPartyContainer;
    public GameObject enemyPartyContainer;

    public BattleState state;
    public BattleSceneScriptable battleScriptable;

    public int turnIndex = -1;
    public int overallTurnNumber;

    public GameObject attacker;
    public string attackerName;
    public GameObject defender;
    public string defenderName;
    public Dictionary<string, Vector3> originalPositions = new Dictionary<string, Vector3>();
    public Dictionary<string, Color> originalSpriteColors = new Dictionary<string, Color>();


    public List<string> playerParty;
    public List<string> enemyParty;
    public List<string> allPlayers;
    public List<string> deadPlayerList;
    public List<Character> characterTurnOrder;


    public string chosenBattleMove;
    public string chosenItem;
    public GameObject spellPrefab;
    public GameObject spellObj;
    public BattleSystemHud battleSystemHud;
    public BattleSystemMenu battleSystemMenu;

    public BattleStateMachine battleStateMachine;
    public BattleBonusManager battleBonusManager;

    public void Start()
    {
        battleSystemHud = new BattleSystemHud();
        battleSystemMenu = new BattleSystemMenu();
        battleBonusManager = new BattleBonusManager();
        Init();
    }

    public void Init()
    {
        playerPrefab = Resources.Load("Prefabs/Characters/Player") as GameObject;

        BattleSystemController.instance.onBattleHudTitleButton += battleSystemHud.OnHUDTitleButton;
      
        if(battleScriptable.enemy != null && battleScriptable.enemy != ""){       
            enemyPrefab = Resources.Load("Prefabs/Characters/" + battleScriptable.enemy) as GameObject;
        }

        GameObject playerGO = initializeParty(ref playerParty, ref playerPrefab, playerBattleStation, playerPartyContainer);
        GameObject enemyGO = initializeParty(ref enemyParty, ref enemyPrefab, enemyBattleStation, enemyPartyContainer, true);
        _SetInitialProperties();

    }

    public GameObject initializeParty(ref List<string> partyList, ref GameObject partyLeaderPrefab, GameObject battleStationContainer, GameObject partyContainer, bool flip=false)
    {
        GameObject partyLeaderObj = Instantiate(partyLeaderPrefab, battleStationContainer.transform);
        partyLeaderObj.transform.SetParent(battleStationContainer.transform);
        if(flip)
            partyLeaderObj.GetComponent<SpriteRenderer>().flipX = true;
        Character partyLeader = partyLeaderPrefab.GetComponent<Character>();
        partyLeaderObj.name = partyLeader.title;
        partyLeader.LoadCharacterClass();
        partyLeader.LoadState();
        partyList.Add(partyLeader.title);
        allPlayers.Add(partyLeader.title);

        battleSystemHud.createSingleHUD(ref partyLeaderObj, ref partyLeader, partyContainer);

        int index = 0;
        foreach(var pm in partyLeader.partyMembers){
            index += 1;
            GameObject partyMemberObject = Instantiate(Resources.Load<GameObject>("Prefabs/" + pm), battleStationContainer.transform);
            Character partyMemberChar = partyMemberObject.GetComponent<Character>();
            partyMemberObject.name = partyMemberChar.title;
            partyMemberChar.LoadCharacterClass();
            partyMemberChar.LoadState();
            partyList.Add(partyMemberChar.title);
            allPlayers.Add(partyMemberChar.title);

            // @todo - right now it puts next party member down 2 * its height. Should try and make this more flexible
            partyMemberObject.transform.SetParent(battleStationContainer.transform);
            partyMemberObject.transform.position = partyMemberObject.transform.position - new Vector3(0.0f, 2 * index, 0.0f);
            if(flip)
                partyMemberObject.GetComponent<SpriteRenderer>().flipX = true;


            battleSystemHud.createSingleHUD(ref partyMemberObject, ref partyMemberChar, partyContainer);
        }
        return partyLeaderObj;
    }

    public void SetAttacker(string attackerName){
        this.attackerName = attackerName;
        this.attacker = GameObject.Find(attackerName);
    }

    public void SetDefender(string defenderName){
        this.defenderName = defenderName;
        this.defender = null;
        if(defenderName!=null && defenderName!=""){
            this.defender = GameObject.Find(defenderName);
        }
    }

    private void _SetInitialProperties(){
        for(int i = 0; i < allPlayers.Count; i++){
            GameObject player = GameObject.Find(allPlayers[i]);
            originalPositions.Add(allPlayers[i], player.transform.position);
            originalSpriteColors.Add(allPlayers[i], player.GetComponent<SpriteRenderer>().color);
        }
    }

}
