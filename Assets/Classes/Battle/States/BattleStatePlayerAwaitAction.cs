using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatePlayerAwaitAction : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;

        _manager.dialogueText.text = "Choose a target for "+_manager.attackerName+":";
        if(_manager.battleSystemHud.selection != null){
            _manager.battleSystemMenu.OpenSubmenu(_manager.battleSystemHud.selection, _manager.battleSystemHud.selectionButton);
            _manager.battleSystemHud.selection = null;
        } else if(_manager.chosenBattleMove != "" & _manager.chosenBattleMove != null) {
            _manager.battleSystemHud.RefreshAllHUDs();
            newState = new BattleStatePlayerAttack();
        } else if(_manager.chosenItem != "" & _manager.chosenItem != null) {
            _manager.battleSystemHud.RefreshAllHUDs();
            newState = new BattleStateUseItem();
        }


        yield return new WaitForSeconds(0f);
        
    }
}
