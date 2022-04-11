using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, // Setup the battle scene
                          PLAYERTURN_START, // Setup the player move
                          PLAYERTURN_AWAIT_MOVE, // Waiting for click event on menu
                          PLAYERTURN_AWAIT_TARGET, // Waiting for click on name button
                          PLAYERTURN_ATTACKING, // Waiting for the attack to complete
                          ENEMYTURN_START, // Setup the enemy move
                          ENEMYTURN_AWAIT_MOVE, // Waiting for AI to select move
                          ENEMYTURN_AWAIT_TARGET, // Waiting for AI to select target
                          ENEMYTURN_ATTACKING, // Waiting for the attack to complete
                          WON, // Player has won the battle
                          LOST // Player has lost the battle
                        }

public class BattleSystem : MonoBehaviour
{

    public Text dialogueText;
    
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public GameObject playerBattleStation;
    public GameObject enemyBattleStation;

    public GameObject playerPartyContainer;
    public GameObject enemyPartyContainer;

    public BattleState state;
    public BattleSceneScriptable battleScriptable;

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
    
    public GameObject initializeParty(ref List<string> partyList, ref GameObject partyLeaderPrefab, GameObject battleStationContainer, GameObject partyContainer)
    {
        GameObject partyLeaderObj = Instantiate(partyLeaderPrefab, battleStationContainer.transform);
        partyLeaderObj.transform.SetParent(battleStationContainer.transform);
        Character partyLeader = partyLeaderPrefab.GetComponent<Character>();
        partyLeaderObj.name = partyLeader.title;
        partyLeader.LoadState();
        Debug.Log("Loaded State of the party leader, current HP is below");
        Debug.Log(partyLeader.currentHP);
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

            createSingleHUD(ref partyMemberObject, ref partyMemberChar, partyContainer);
        }
        return partyLeaderObj;
    }

    public void RefreshAllHUDs()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("BattleHUD");
        foreach(var hud in objs){           
            hud.GetComponent<BattleHUD>().Refresh();
        }
    }

    IEnumerator SetupBattle() 
    {
        GameObject playerGO = initializeParty(ref playerParty, ref playerPrefab, playerBattleStation, playerPartyContainer);
        playerUnit = playerGO.GetComponent<Character>();

        GameObject enemyGO = initializeParty(ref enemyParty, ref enemyPrefab, enemyBattleStation, enemyPartyContainer);
        enemyUnit = enemyGO.GetComponent<Character>();

        dialogueText.text = enemyUnit.title + " engages in battle...";

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN_START;
        PlayerTurnStart();

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

    public Character GetNextEnemy(Character currentEnemy)
    {
        int currentEnemyIndex = enemyParty.IndexOf(currentEnemy.name);
        for(int i = 0; i < enemyParty.Count; i++)
        {
            int indexToCheck = (currentEnemyIndex + i + 1) % enemyParty.Count;
            Character enemy = GetCharacter(enemyParty[indexToCheck]);
            if(enemy.currentHP > 0)
                return GetCharacter(enemyParty[indexToCheck]);
        }
        return currentEnemy;
    }

    public bool PartyDead(List<string> partyMembers)
    {
        Debug.Log("Calling Party dead with following partyMembers");
        Debug.Log(partyMembers);
        foreach(var id in partyMembers)
        {
            Debug.Log("Checking the following members HP");
            Character member = GetCharacter(id);
            Debug.Log(member.title);
            Debug.Log(member.currentHP);
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
            if(isDead)
            {
                bool allDead = PartyDead(enemyParty);
                if(allDead)
                {
                    state = BattleState.WON;
                    EndBattle();
                } else {
                    enemyUnit = GetNextEnemy(enemyUnit);
                    state = BattleState.ENEMYTURN_START;
                    EnemyTurnStart();
                }
            }
            else 
            {
                state = BattleState.ENEMYTURN_START;
                EnemyTurnStart();
            }
        }
        else {
            dialogueText.text = playerUnit.title + " cannot choose this attack";
            yield return new WaitForSeconds(2f);
            PlayerTurnStart();
        }
        
    }

    // // this should exist somewhere else, similar to attack
    // IEnumerator PlayerHeal()
    // {
    //     playerUnit.Heal(5);

    //     RefreshAllHUDs();
    //     dialogueText.text = "You have healed!";

    //     yield return new WaitForSeconds(2f);

    //     state = BattleState.ENEMYTURN;
    //     StartCoroutine(EnemyTurn());
    // }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.title + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(5); // enemyParty[currentEnemy].damage);

        RefreshAllHUDs();

        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            bool allDead = PartyDead(playerParty);
            if(allDead)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            else
            {
                state = BattleState.PLAYERTURN_START;
                PlayerTurnStart();
            }

        } else 
        {
            state = BattleState.PLAYERTURN_START;
            PlayerTurnStart();
        }
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
        }   
        Character player = GameObject.FindWithTag("Player").GetComponent<Character>();
        player.SaveState();
        Invoke("ReturnToWorld", 3);
    }

    public void OnHUDTitleButton(string characterID)
    {
        Character character = GetCharacter(characterID);
        if(state == BattleState.PLAYERTURN_AWAIT_MOVE)
        {
            playerUnit = character;
            OpenSubmenu(character);
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
        dialogueText.text = "Choose an action:";
        state = BattleState.PLAYERTURN_AWAIT_MOVE;
    }

    void EnemyTurnStart()
    {
        dialogueText.text = enemyUnit.title + " is starting their attack!";
        state = BattleState.ENEMYTURN_AWAIT_MOVE;
        StartCoroutine(EnemyTurn());
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

    public void OpenSubmenu(Character character)
    {
        createOptionSubmenu(character);
    }

    public void closeOptionSubmenu()
    {
        GameObject obj = GameObject.Find("Menu(Clone)");
        DynamicMenu menu = obj.GetComponent<DynamicMenu>();
        menu.Close();
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
        {"Attacks", delegate { menu.SubMenu(attacks); }},
        {"Spells", delegate { menu.SubMenu(spells); }},
        {"Items", delegate { menu.SubMenu(items); }},
        {"Strategies", delegate { menu.SubMenu(strategies); }},
        {"Return", () => {}},
    });
    }    

}
