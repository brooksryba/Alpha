using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatePlayerStart : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;
        battleObjManager.battleSystemHud.disableUnusableHuds(battleObjManager.playerUnit.title, battleObjManager.playerParty);

        battleObjManager.dialogueText.text = "It's "+battleObjManager.playerUnit.title+" turn to attack!";
        battleObjManager.battleSystemHud.canSelect = true;

        if(battleObjManager.battleSystemHud.selection != null){
            battleObjManager.battleSystemHud.canSelect = false;
            newState = new BattleStatePlayerAwaitAction();
        }

        yield return new WaitForSeconds(0f);
    }
}
