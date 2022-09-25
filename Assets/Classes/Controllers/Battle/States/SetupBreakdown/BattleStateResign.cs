using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class BattleStateResign : BattleState
{
    public override IEnumerator enter()
    {
        Toast("You resigned the battle");
        GameObject.FindWithTag("Player").GetComponent<Character>().currentHP = 0;
        return base.enter(3f);
    }
    override public IEnumerator execute()
    {
        SaveSystem.instance.Deregister();
        SceneManager.LoadScene(sceneName: _manager.battleScriptable.scene);
        return base.execute();
    }
}
