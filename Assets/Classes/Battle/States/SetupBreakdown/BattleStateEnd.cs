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

        if(battleSystemUtils.PartyDead(_manager.charManager.enemyParty))
        {
            _manager.dialogueText.text = "You won the battle!";
        } else if (battleSystemUtils.PartyDead(_manager.charManager.playerParty))
        {
            _manager.dialogueText.text = "You were defeated";
        } 


        _manager.battleBonusManager.DestroyAllBonuses();

        yield return new WaitForSeconds(2f);

        SaveSystem.SaveAndDeregister();
        SceneManager.LoadScene(sceneName: battleObjManager.battleScriptable.scene);
        yield return new WaitForSeconds(0f);
    }
}
