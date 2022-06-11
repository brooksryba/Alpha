using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateSetup : BattleState
{

    override public IEnumerator execute()
    {
        //battleObjManager.Init();
        battleObjManager.battleSystemHud.RefreshAllHUDs();

        battleObjManager.dialogueText.text = battleObjManager.enemyUnit.title + " engages in battle...";

        yield return new WaitForSeconds(1f);

        newState = new BattleStateGetAttacker();
    }


        
}
