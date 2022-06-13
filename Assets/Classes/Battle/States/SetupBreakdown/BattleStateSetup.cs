using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateSetup : BattleState
{

    override public IEnumerator execute()
    {
        //_manager.Init();
        _manager.battleSystemHud.RefreshAllHUDs();

        _manager.dialogueText.text = _manager.charManager.enemyParty[0] + " engages in battle...";

        yield return new WaitForSeconds(1f);

        newState = new BattleStateGetAttacker();
    }


        
}
