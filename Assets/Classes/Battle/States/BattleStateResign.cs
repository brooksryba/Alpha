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

        battleObjManager.dialogueText.text = "You resigned the battle";

        GameObject.Find("EnemyBattleStation").transform.GetChild(0).GetComponent<Character>().dialogIndex = 0;

        GameObject.FindWithTag("Player").GetComponent<Character>().currentHP = 0;

        battleObjManager.battleSystemHud.RefreshAllHUDs();

        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(sceneName:"World");
    }
}
