using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public GameObject playerBattleStation;
    public GameObject enemyBattleStation;

    public GameObject playerPartyContainer;
    public GameObject enemyPartyContainer;

    Character playerUnit;
    Character enemyUnit;

    public Text dialogueText;

    public BattleState state;
    public BattleSceneScriptable battleScriptable;

    void Start()
    {
        if(battleScriptable.enemy != null && battleScriptable.enemy != ""){           
            enemyPrefab = Resources.Load("Prefabs/Characters/" + battleScriptable.enemy) as GameObject;
        }
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    public void createSingleHUD(ref GameObject partyMember, GameObject partyContainer)
    {
        GameObject battleHudPrefab = Instantiate(Resources.Load<GameObject>("Prefabs/BattleHUD"), partyContainer.transform);
        battleHudPrefab.transform.SetParent(partyContainer.transform);
        BattleHUD hud = battleHudPrefab.GetComponent<BattleHUD>();
        hud.character = partyMember.GetComponent<Character>();
        hud.Refresh();
    }
    public GameObject initializeParty(GameObject partyLeaderPrefab, GameObject battleStationContainer, GameObject partyContainer)
    {
        GameObject partyLeaderObj = Instantiate(partyLeaderPrefab, battleStationContainer.transform);
        partyLeaderObj.transform.SetParent(battleStationContainer.transform);
        Character partyLeader = partyLeaderPrefab.GetComponent<Character>();
        partyLeader.LoadState();

        createSingleHUD(ref partyLeaderObj, partyContainer);

        int index = 0;
        foreach(var pm in partyLeader.partyMembers){
            index += 1;
            GameObject partyMemberObject = Instantiate(Resources.Load<GameObject>("Prefabs/" + pm), battleStationContainer.transform);
            partyMemberObject.GetComponent<Character>().LoadState();

            // @todo - right now it puts next party member down 2 * its height. Should try and make this more flexible
            partyMemberObject.transform.SetParent(battleStationContainer.transform);
            partyMemberObject.transform.position = partyMemberObject.transform.position - new Vector3(0.0f, 2 * index, 0.0f);

            createSingleHUD(ref partyMemberObject, partyContainer);
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
        GameObject playerGO = initializeParty(playerPrefab, playerBattleStation, playerPartyContainer);
        playerUnit = playerGO.GetComponent<Character>();

        GameObject enemyGO = initializeParty(enemyPrefab, enemyBattleStation, enemyPartyContainer);
        enemyUnit = enemyGO.GetComponent<Character>();

        dialogueText.text = enemyUnit.title + " engages in battle...";

        yield return new WaitForSeconds(1f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    IEnumerator PlayerAttack(Func<Character, Character, bool> AttackName, Character self, Character enemy)
    {
        bool isAccepted = AttackName(self, enemy);
        bool isDead = enemyUnit.currentHP <= 0;

        RefreshAllHUDs();
        if(isAccepted){
            
            dialogueText.text = "The attack is successful";
            RefreshAllHUDs();
            yield return new WaitForSeconds(2f);
            if(isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else 
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
        else {
            dialogueText.text = "You cannot choose this attack";
        }
        
    }

    // this should exist somewhere else, similar to attack
    IEnumerator PlayerHeal()
    {
        playerUnit.Heal(5);

        RefreshAllHUDs();
        dialogueText.text = "You have healed!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.title + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);


        RefreshAllHUDs();

        yield return new WaitForSeconds(1f);

        if(isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        } else 
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
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
        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        player.SaveState();
        Invoke("ReturnToWorld", 3);
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose an action:";
    }

    public void OnAttackButton(Func<Character, Character, bool> AttackName, Character self, Character enemy)
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack(AttackName, self, enemy));
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }

}
