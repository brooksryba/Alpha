using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatePlayerAwaitAction : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;

        battleObjManager.dialogueText.text = "Choose a target for "+battleObjManager.playerUnit.title+":";
        if(battleObjManager.battleSystemHud.selection != null){
            battleObjManager.battleSystemMenu.OpenSubmenu(battleObjManager.battleSystemHud.selection, battleObjManager.battleSystemHud.selectionButton);
            battleObjManager.battleSystemHud.selection = null;
        } else if(battleObjManager.chosenBattleMove != "" & battleObjManager.chosenBattleMove != null) {
            battleObjManager.battleSystemHud.RefreshAllHUDs();
            newState = new BattleStatePlayerAttack();
        } else if(battleObjManager.chosenItem != "" & battleObjManager.chosenItem != null) {
            battleObjManager.battleSystemHud.RefreshAllHUDs();
            newState = new BattleStateUseItem();
        }


        yield return new WaitForSeconds(0f);
        
    }
}