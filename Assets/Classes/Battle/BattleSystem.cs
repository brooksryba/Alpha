using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public enum BattleState { START, // Setup the battle scene
                          DETERMINE_NEXT_ATTACKER, // Figure out which character gets the next move
                          PLAYERTURN_START, // Setup the player move
                          PLAYERTURN_AWAIT_MOVE, // Waiting for click event on menu
                          PLAYERTURN_AWAIT_TARGET, // Waiting for click on name button
                          PLAYERTURN_ATTACKING, // Waiting for the attack to complete
                          ENEMYTURN_START, // Setup the enemy move
                          ENEMYTURN_AWAIT_MOVE, // Waiting for AI to select move
                          ENEMYTURN_AWAIT_TARGET, // Waiting for AI to select target
                          ENEMYTURN_ATTACKING, // Waiting for the attack to complete
                          RESIGN, // Player has resigned the battle
                          WON, // Player has won the battle
                          LOST // Player has lost the battle
                        }

public class BattleSystem : MonoBehaviour
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

    Character playerUnit;
    Character enemyUnit;

    List<string> playerParty;
    List<string> enemyParty;

    AttackData attackReference;

    void Start()
    {
        BattleSystemController.instance.onBattleHudTitleButton += OnHUDTitleButton;

        if(battleScriptable.enemy != null && battleScriptable.enemy != ""){           
            enemyPrefab = Resources.Load("Prefabs/Characters/" + battleScriptable.enemy) as GameObject;
        }

        playerParty = new List<string>();
        enemyParty = new List<string>();

        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    public Character GetCharacter(string id)
    {
        return GameObject.Find(id).GetComponent<Character>();
    }

    public void createSingleHUD(ref GameObject partyMember, ref Character hudCharacter, GameObject partyContainer)
    {
        GameObject battleHudPrefab = Instantiate(Resources.Load<GameObject>("Prefabs/BattleHUD"), partyContainer.transform);
        battleHudPrefab.transform.SetParent(partyContainer.transform);
        BattleHUD hud = battleHudPrefab.GetComponent<BattleHUD>();
        hud.character = hudCharacter;
        hud.Refresh();        
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

        createSingleHUD(ref partyLeaderObj, ref partyLeader, partyContainer);

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


            createSingleHUD(ref partyMemberObject, ref partyMemberChar, partyContainer);
        }
        return partyLeaderObj;
    }

    public void RefreshAllHUDs()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("BattleHUD");
        foreach(var hud in objs){
            BattleHUD battleHud = hud.GetComponent<BattleHUD>();
            Character updateHudCharacter = GetCharacter(battleHud.character.title);
            battleHud.character = updateHudCharacter;
            battleHud.Refresh();
        }
    }

    IEnumerator SetupBattle() 
    {
        GameObject playerGO = initializeParty(ref playerParty, ref playerPrefab, playerBattleStation, playerPartyContainer);
        playerUnit = playerGO.GetComponent<Character>();

        GameObject enemyGO = initializeParty(ref enemyParty, ref enemyPrefab, enemyBattleStation, enemyPartyContainer, true);
        enemyUnit = enemyGO.GetComponent<Character>();

        dialogueText.text = enemyUnit.title + " engages in battle...";

        yield return new WaitForSeconds(1f);

        state = BattleState.DETERMINE_NEXT_ATTACKER;
        GetNextAttacker();

    }

    public void disableUnusableHuds(string usableHudName){
        // this should probably be done in a HUD manager or equivalent 
        GameObject[] objs = GameObject.FindGameObjectsWithTag("BattleHUD");
        foreach(var hud in objs){
            BattleHUD battleHud = hud.GetComponent<BattleHUD>();
            if (playerParty.Contains(battleHud.character.title)){
            if (battleHud.character.title==usableHudName)
                continue;
            else 
                battleHud.playerButton.interactable = false;
            }

        }
    }
    public void GetNextAttacker()
    {
        // Loops through ordered list of all characters by speed and selects the next in order to attack
        // Potential concerns - Are we allowing speed changes mid-battle, if so how do we handle that? Currently, the order of the list will change, but turn number does not reset

        if(state==BattleState.WON || state==BattleState.LOST)
            return;
        turnIndex += 1;
        int totalPlayers = playerParty.Count + enemyParty.Count;
        if(turnIndex >= totalPlayers)
            turnIndex = 0;
        List<Character> allCharacters = new List<Character>();
        foreach(var p in playerParty){
            allCharacters.Add(GetCharacter(p));
        }
        foreach(var p in enemyParty){
            allCharacters.Add(GetCharacter(p));
        }

        allCharacters.Sort(delegate(Character a, Character b){
            return (b.speed).CompareTo(a.speed); // highest speed first (a comp to b is lowest)
        });

        for(int i = 0; i < allCharacters.Count; i++){
            int indexToCheck = (turnIndex + i) % allCharacters.Count;
            if(allCharacters[indexToCheck].currentHP > 0){
                turnIndex = indexToCheck;
                break;
            }
        }
        Character nextUp = allCharacters[turnIndex];
        if(playerParty.Contains(nextUp.title)){
            state = BattleState.PLAYERTURN_START;
            dialogueText.text = "It is "+nextUp.title+"'s turn to attack!";
            playerUnit = nextUp;
            disableUnusableHuds(nextUp.title);
            PlayerTurnStart();
        } else {
            state = BattleState.ENEMYTURN_START;
            dialogueText.text = "It is "+nextUp.title+"'s turn to attack!";
            enemyUnit = nextUp;
            StartCoroutine(EnemyTurn());
        }
            
    }

    public bool DoAttack(AttackData attack, ref Character attacker, ref Character defender)
    {
        if(attacker.useMana(attack.mana)){
            defender.TakeDamage(attack.damage);
            return true;
        } else {
            return false;
        }        
    }

    public bool PartyDead(List<string> partyMembers)
    {
        foreach(var id in partyMembers)
        {
            Character member = GetCharacter(id);
            if(member.currentHP > 0)
                return false;
        }
        return true;
    }

    IEnumerator PlayerAttack()
    {
        bool isAccepted = DoAttack(attackReference, ref playerUnit, ref enemyUnit);
        bool isDead = enemyUnit.currentHP <= 0;

        RefreshAllHUDs();
        if(isAccepted){
            
            dialogueText.text = "The attack is successful";
            RefreshAllHUDs();
            yield return new WaitForSeconds(2f);
            if(isDead){
                bool allDead = PartyDead(enemyParty);
                if(allDead){
                    state = BattleState.WON;
                    EndBattle();
                }
            }
            GetNextAttacker();
        }
        else {
            dialogueText.text = playerUnit.title + " cannot choose this attack";
            yield return new WaitForSeconds(2f);
            PlayerTurnStart();
        }
        
    }

    IEnumerator EnemyTurn()
    {
        state = BattleState.ENEMYTURN_AWAIT_MOVE; // state changes will be needed when AI is implemented, but not useful yet
        state = BattleState.ENEMYTURN_AWAIT_TARGET;
        dialogueText.text = enemyUnit.title + " attacks " + playerUnit.title + "!";

        yield return new WaitForSeconds(1f);
        // @todo - this is where the enemies AI should be implemented

        state = BattleState.ENEMYTURN_ATTACKING;
        bool isDead = playerUnit.TakeDamage(5); // enemyParty[currentEnemy].damage);

        RefreshAllHUDs();

        yield return new WaitForSeconds(1f);

        if(isDead){
            bool allDead = PartyDead(playerParty);
            if(allDead){
                state = BattleState.LOST;
                EndBattle();
            }
        }

        GetNextAttacker();
    }

    void ReturnToWorld()
    {
        SceneManager.LoadScene (sceneName:"World");
    }

    void EndBattle()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        } else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated";
        } else if (state == BattleState.RESIGN) {
            dialogueText.text = "You resigned the battle";
        }

        if(state != BattleState.RESIGN) {
            GameObject.FindWithTag("Player").GetComponent<Character>().SaveState();
            GameObject.Find("EnemyBattleStation").transform.GetChild(0).GetComponent<Character>().SaveState();

            foreach(GameObject go in GameObject.FindGameObjectsWithTag("Friendly")) {
                go.GetComponent<Character>().SaveState();
            }
        }

        Invoke("ReturnToWorld", 3);
    }

    public void OnHUDTitleButton(string characterID, GameObject target)
    {
        Character character = GetCharacter(characterID);
        if(state == BattleState.PLAYERTURN_AWAIT_MOVE)
        {
            playerUnit = character;
            OpenSubmenu(character, target);
        }   
        else if(state == BattleState.PLAYERTURN_AWAIT_TARGET)
        {
            enemyUnit = character;
            state = BattleState.PLAYERTURN_ATTACKING;
            PlayerTurnAttack();
        }
    }

    void PlayerTurnStart()
    {
        state = BattleState.PLAYERTURN_AWAIT_MOVE;
    }


    void AwaitTarget()
    {
        closeOptionSubmenu();

        dialogueText.text = "Choose a target for "+playerUnit.title+":";
        state = BattleState.PLAYERTURN_AWAIT_TARGET;
    }

    void PlayerTurnAttack()
    {
        if (state != BattleState.PLAYERTURN_ATTACKING)
            return;

        StartCoroutine(PlayerAttack());        
    }

    public void OpenSubmenu(Character character, GameObject target)
    {
        closeOptionSubmenu();
        createOptionSubmenu(character);
        positionOptionSubmenu(target);
    }

    public void positionOptionSubmenu(GameObject target)
    {
        GameObject obj = GameObject.Find("MenuList");
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        obj.transform.position = cam.WorldToScreenPoint(target.transform.position);
        obj.transform.position += new Vector3(0, 100, 0);
    }

    public void closeOptionSubmenu()
    {
        GameObject obj = GameObject.Find("Menu(Clone)");
        if(obj) 
        {
            Destroy(obj);
        }
    }

    public void createOptionSubmenu(Character character)
    {
        GameObject obj = Instantiate(Resources.Load("Prefabs/Menu"), transform.position, transform.rotation) as GameObject;
        DynamicMenu menu = obj.GetComponent<DynamicMenu>();

        Dictionary<string, Action> attacks = new Dictionary<string, Action>();
        Dictionary<string, Action> spells = new Dictionary<string, Action>();
        Dictionary<string, Action> strategies = new Dictionary<string, Action>();

        foreach( var attackRef in character.getAttacks() ) {
            attacks.Add(attackRef.Key, () => { attackReference = attackRef.Value; AwaitTarget();});
        }
        attacks.Add("Return", () => { });
        
        spells.Add("Heal", () => { });
        spells.Add("Fire Damage", () => { });
        spells.Add("Return", () => { });

        strategies.Add("Charge Mana", () => { });
        strategies.Add("Return", () => { });

        Dictionary<string, Action> items = new Dictionary<string, Action>();
        if(character.GetComponent<Player>() != null){
            foreach (var item in character.GetComponent<Player>().items)
            {
                items.Add(item.title, () => { });
            }
        }
        items.Add("Return", () => { });

        menu.Open(new Dictionary<string, Action>(){
        {"Attacks >>", delegate { menu.SubMenu(attacks); }},
        {"Spells >>", delegate { menu.SubMenu(spells); }},
        {"Items >>", delegate { menu.SubMenu(items); }},
        {"Strategies >>", delegate { menu.SubMenu(strategies); }},
        {"Resign", delegate { state = BattleState.RESIGN; EndBattle(); }},
        {"Return", () => {}},
        });
    
    
    }    

}
