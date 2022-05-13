using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatePlayerAwaitAttack : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;

        battleObjManager.dialogueText.text = "Choose a target for "+battleObjManager.playerUnit.title+":";

        if(battleObjManager.battleSystemHud.selection != null){
            battleObjManager.battleSystemMenu.OpenSubmenu(battleObjManager.battleSystemHud.selection, battleObjManager.battleSystemHud.selectionButton);
            battleObjManager.battleSystemHud.selection = null;
        } else if(battleObjManager.attackReference != null) {
            newState = new BattleStatePlayerAttack();
        }

        yield return new WaitForSeconds(0f);
        
    }
}
