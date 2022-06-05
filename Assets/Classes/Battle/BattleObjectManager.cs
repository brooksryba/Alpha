using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleObjectManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public GameObject playerBattleStation;
    public GameObject enemyBattleStation;

    public GameObject playerPartyContainer;
    public GameObject enemyPartyContainer;

    public BattleState state;
    public BattleSceneScriptable battleScriptable;

    public int turnIndex = -1;

    public Character playerUnit;
    public Character enemyUnit;

    public GameObject attacker;
    public Vector3 attackerPositionStart; // this can be a list of Vector2s, think this through for spread attacks
    public GameObject defender;
    public Vector3 defenderPositionStart;

    public List<string> playerParty;
    public List<string> enemyParty;

    public string chosenAttack;
    public BattleSystemHud battleSystemHud;
    public BattleSystemMenu battleSystemMenu;

    public BattleStateMachine battleStateMachine;

    public void Start()
    {
        battleSystemHud = new BattleSystemHud();
        battleSystemMenu = new BattleSystemMenu();
    }

    public void Init()
    {
        playerPrefab = Resources.Load("Prefabs/Characters/Player") as GameObject;

        BattleSystemController.instance.onBattleHudTitleButton += battleSystemHud.OnHUDTitleButton;
      
        if(battleScriptable.enemy != null && battleScriptable.enemy != ""){       
            enemyPrefab = Resources.Load("Prefabs/Characters/" + battleScriptable.enemy) as GameObject;
        }

        GameObject playerGO = initializeParty(ref playerParty, ref playerPrefab, playerBattleStation, playerPartyContainer);
        playerUnit = playerGO.GetComponent<Character>();

        GameObject enemyGO = initializeParty(ref enemyParty, ref enemyPrefab, enemyBattleStation, enemyPartyContainer, true);
        enemyUnit = enemyGO.GetComponent<Character>();
    }

    public GameObject initializeParty(ref List<string> partyList, ref GameObject partyLeaderPrefab, GameObject battleStationContainer, GameObject partyContainer, bool flip=false)
    {
        GameObject partyLeaderObj = Instantiate(partyLeaderPrefab, battleStationContainer.transform);
        partyLeaderObj.transform.SetParent(battleStationContainer.transform);
        if(flip)
            partyLeaderObj.GetComponent<SpriteRenderer>().flipX = true;
        Character partyLeader = partyLeaderPrefab.GetComponent<Character>();
        partyLeaderObj.name = partyLeader.title;
        partyLeader.LoadState();
        partyList.Add(partyLeader.title);

        battleSystemHud.createSingleHUD(ref partyLeaderObj, ref partyLeader, partyContainer);

        int index = 0;
        foreach(var pm in partyLeader.partyMembers){
            index += 1;
            GameObject partyMemberObject = Instantiate(Resources.Load<GameObject>("Prefabs/" + pm), battleStationContainer.transform);
            Character partyMemberChar = partyMemberObject.GetComponent<Character>();
            partyMemberObject.name = partyMemberChar.title;
            partyMemberChar.LoadState();
            partyList.Add(partyMemberChar.title);

            // @todo - right now it puts next party member down 2 * its height. Should try and make this more flexible
            partyMemberObject.transform.SetParent(battleStationContainer.transform);
            partyMemberObject.transform.position = partyMemberObject.transform.position - new Vector3(0.0f, 2 * index, 0.0f);
            if(flip)
                partyMemberObject.GetComponent<SpriteRenderer>().flipX = true;


            battleSystemHud.createSingleHUD(ref partyMemberObject, ref partyMemberChar, partyContainer);
        }
        return partyLeaderObj;
    }

}
