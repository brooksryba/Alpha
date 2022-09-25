using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatePlayerStart : BattleState
{
    public override IEnumerator enter()
    {
        Toast("It's " + _manager.charManager.attackerName + "'s turn to attack!");

        _manager.battleSystemHud.canSelect = true;
        _manager.battleSystemHud.disableUnusableHuds(_manager.charManager.attackerName, _manager.charManager.playerParty);
        return base.enter();
    }

    override public IEnumerator execute()
    {
        if(_manager.battleSystemHud.selection != null && _manager.battleSystemHud.selection.title == _manager.charManager.attackerName){
            _manager.battleSystemHud.canSelect = false;
            Transition(new BattleStatePlayerAwaitAction());
        }

        return base.execute();
    }
}
