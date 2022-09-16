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
            newMessage = "You won the battle!";
        } else if (battleSystemUtils.PartyDead(_manager.charManager.playerParty))
        {
            newMessage = "You were defeated";
        } 


        _manager.battleBonusManager.DestroyAllBonuses();

        yield return new WaitForSeconds(2f);

        SaveSystem.SaveAndDeregister();
        SceneManager.LoadScene(sceneName: _manager.battleScriptable.scene);
        yield return new WaitForSeconds(0f);
    }
}
