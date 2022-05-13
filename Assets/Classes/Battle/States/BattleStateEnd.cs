using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class BattleStateEnd : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;

        if(battleSystemUtils.PartyDead(battleObjManager.enemyParty))
        {
            battleObjManager.dialogueText.text = "You won the battle!";
        } else if (battleSystemUtils.PartyDead(battleObjManager.playerParty))
        {
            battleObjManager.dialogueText.text = "You were defeated";
        } 

        GameObject.FindWithTag("Player").GetComponent<Character>().SaveState();
        GameObject.Find("EnemyBattleStation").transform.GetChild(0).GetComponent<Character>().SaveState();

        foreach(GameObject go in GameObject.FindGameObjectsWithTag("Friendly")) {
            go.GetComponent<Character>().SaveState();
        }

        SceneManager.LoadScene(sceneName:"World");
        yield return new WaitForSeconds(0f);
    }
}
