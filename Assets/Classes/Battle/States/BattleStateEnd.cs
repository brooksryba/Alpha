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

        battleObjManager.battleBonusManager.DestroyAllBonuses();

        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(sceneName:"World");
        yield return new WaitForSeconds(0f);
    }
}
