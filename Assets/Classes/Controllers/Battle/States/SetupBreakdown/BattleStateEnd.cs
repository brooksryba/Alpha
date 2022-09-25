using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class BattleStateEnd : BattleState
{
    public override IEnumerator enter()
    {
        Toast(battleSystemUtils.PartyDead(_manager.charManager.enemyParty) ? "You won the battle!" : "You were defeated");
        _manager.battleBonusManager.DestroyAllBonuses();
        return base.enter(2f);
    }
    override public IEnumerator execute()
    {
        SaveSystem.instance.SaveAndDeregister();
        SceneManager.LoadScene(sceneName: _manager.battleScriptable.scene);
        return base.execute();
    }
}
