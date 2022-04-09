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

    public Transform playerBattleStation;
    public Transform enemyBattleStation;

    Character playerUnit;
    Character enemyUnit;

    public Text dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;
    public BattleSceneScriptable battleScriptable;

    void Start()
    {
        if(battleScriptable.enemy != null){
            enemyPrefab = Resources.Load("Prefabs/" + battleScriptable.enemy) as GameObject;
        }
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle() 
    {


        GameObject playerGO = Instantiate(playerPrefab, playerBattleStation);
        playerUnit = playerGO.GetComponent<Player>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Enemy>();

        dialogueText.text = enemyUnit.name + " engages in battle...";

        yield return new WaitForSeconds(1f);

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        state = BattleState.PLAYERTURN;
        PlayerTurn();

    }

    IEnumerator PlayerAttack(Func<Character, Character, bool> AttackName, Character self, Character enemy)
    {
        bool isAccepted = AttackName(self, enemy);
        bool isDead = enemyUnit.currentHP <= 0;

        enemyHUD.SetHP(enemyUnit.currentHP);
        if(isAccepted){
            
            dialogueText.text = "The attack is successful";
            playerHUD.SetHUD(playerUnit);
            enemyHUD.SetHUD(enemyUnit);
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

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "You have healed!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.name + " attacks!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.damage);


        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

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
