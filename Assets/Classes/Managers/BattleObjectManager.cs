using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleObjectManager : MonoBehaviour
{
    private static BattleObjectManager _instance;
    public static BattleObjectManager instance { get { return _instance; } }

    public GameObject playerPrefab; // should be deleted
    public GameObject enemyPrefab;  // should be deleted

    public GameObject playerBattleStation; // breaking out List<GameObject> > transforms set to each battle location
    public GameObject enemyBattleStation;   // should be deleted once the above is set to a list

    public GameObject playerPartyContainer; // these are for the HUD, consider making this a list to populate
    public GameObject enemyPartyContainer;
    public List<int> allCharactersNew;   // should be deleted

    public BattleCondition condition = new BattleCondition();

    // public string chosenBattleMove; // should be deleted
    public bool playerResigned;
    public Item chosenItem;
    public Move chosenMove;

    public List<GameObject> prefabs;

    public BattleSystemHud battleSystemHud; // this will likely be refactored, will be a gameobject / Monobehaviour, likely to refresh automatically
    public BattleSystemMenu battleSystemMenu;

    private void Awake() { _instance = this; }

    public void Start()
    {
        battleSystemHud = new BattleSystemHud();
        battleSystemMenu = new BattleSystemMenu();
        Init();
    }

    public void Init()
    {
        playerPrefab = Resources.Load("Prefabs/Characters/Player") as GameObject;

        if(SceneSystem.battle == null)
            SceneSystem.battle = new BattleData("Livar", "World_Main_2", null);

        BattleSystemController.instance.onBattleHudTitleButton += battleSystemHud.OnHUDTitleButton;
      
        if(SceneSystem.battle != null && SceneSystem.battle.enemy != null && SceneSystem.battle.enemy != ""){       
            enemyPrefab = Resources.Load("Prefabs/Characters/" + SceneSystem.battle.enemy) as GameObject;
        }


        List<int> friendlyListPlaceholder = new List<int>();
        List<int> enemyListPlaceholder = new List<int>();
        friendlyListPlaceholder.AddRange(new int[]{0, 1, 2});
        enemyListPlaceholder.AddRange(new int[]{3, 4, 5});
        initializeParty(friendlyListPlaceholder, playerBattleStation, playerPartyContainer);
        initializeParty(enemyListPlaceholder, enemyBattleStation, enemyPartyContainer, true);
        _SetInitialProperties();

    }

    public void initializeParty(List<int> partyList, GameObject battleStationContainer, GameObject partyContainer, bool flip=false)
    {
        int index = 0;
        foreach(int pm in partyList){
            GameObject member = PrefabManager.Load(battleStationContainer.transform, pm, PrefabManager.Types.Character);
            
            
            prefabs.Add(member);

            // GameObject partyMemberObject = Instantiate(Resources.Load<GameObject>("Prefabs/" + pm), battleStationContainer.transform);
            Character partyMemberChar = CharacterManager.Get(pm);

            // @todo - right now it puts next party member down 2 * its height. Should try and make this more flexible
            member.transform.SetParent(battleStationContainer.transform);
            member.transform.position = member.transform.position - new Vector3(0.0f, 2 * index, 0.0f);
            if(flip)
                member.GetComponent<SpriteRenderer>().flipX = true;


            battleSystemHud.createSingleHUD(ref member, ref partyMemberChar, partyContainer);
            index += 1;
        }

    }


    public void IncrementPlayerTurn(int playerID){
        // TODO - this should handle battle effects
        // For handling battle effects
        // for(int i = 0; i < battleBonuses.Count; i++){
        //     BattleBonus checkBonus = battleBonuses[i];
        //     if(checkBonus.playerName==playerName){
        //         battleBonuses[i].bonusDuration -= 1;
        //         battleBonuses[i].BattleBonusAction();
        //         if(battleBonuses[i].bonusDuration==0){
        //             battleBonuses.RemoveAt(i);
        //             i--; //ensures that if multiple are removed, it removes the correct bonuses
        //         }
        //     }
        // }
    }

    // TODO - cleanup java style get/set and use accessor props with public
    public void SetAttacker(string attackerName){
        condition.attackerName = attackerName;
        condition.attacker = GameObject.Find(attackerName);
    }

    public void SetDefender(string defenderName){
        condition.defenderName = defenderName;
        condition.defender = null;
        if(defenderName!=null && defenderName!=""){
            condition.defender = GameObject.Find(defenderName);
        }
    }

    private void _SetInitialProperties(){
        for(int i = 0; i < condition.allPlayers.Count; i++){
            GameObject player = GameObject.Find(CharacterManager.Get(condition.allPlayers[i]).title);
            condition.originalPositions.Add(condition.allPlayers[i], player.transform.position);
            condition.originalSpriteColors.Add(condition.allPlayers[i], player.GetComponent<SpriteRenderer>().color);
        }
    }

}
