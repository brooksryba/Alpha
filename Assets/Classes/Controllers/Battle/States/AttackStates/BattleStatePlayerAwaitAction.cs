using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatePlayerAwaitAction : BattleState
{
    public override IEnumerator enter()
    {
        Toast("Choose an action for "+_manager.charManager.attackerName+":");
        return base.enter();
    }

    override public IEnumerator execute()
    {
        if(_manager.battleSystemHud.selection != null){
            _manager.battleSystemMenu.OpenSubmenu(_manager.battleSystemHud.selection, _manager.battleSystemHud.selectionButton);
            _manager.battleSystemHud.selection = null;
        } else if(_manager.chosenBattleMove != "" & _manager.chosenBattleMove != null) {
            _manager.battleSystemHud.RefreshAllHUDs();
            Transition(new BattleStatePlayerAttack());
        } else if(_manager.chosenItem != "" & _manager.chosenItem != null) {
            _manager.battleSystemHud.RefreshAllHUDs();
            Transition(new BattleStateUseItem());
        } else if(
            (_manager.battleSystemHud.selection == null) && 
            ((_manager.chosenBattleMove == "") && (_manager.chosenItem == "")) &&
            (GameObject.Find("Menu(Clone)") == null)) {
                Transition(new BattleStatePlayerStart());
            }

        return base.execute();
    }
}
