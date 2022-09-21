using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class BattleStateResign : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;

        newMessage = "You resigned the battle";

        GameObject.FindWithTag("Player").GetComponent<Character>().currentHP = 0;

        _manager.battleSystemHud.RefreshAllHUDs();

        yield return new WaitForSeconds(3f);

        SaveSystem.instance.Deregister();
        SceneManager.LoadScene(sceneName: _manager.battleScriptable.scene);
    }
}
