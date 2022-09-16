using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatePlayerStart : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;
        _manager.battleSystemHud.disableUnusableHuds(_manager.charManager.attackerName, _manager.charManager.playerParty);

        newMessage = "It's " + _manager.charManager.attackerName + "'s turn to attack!";
        _manager.battleSystemHud.canSelect = true;

        if(_manager.battleSystemHud.selection != null){
            _manager.battleSystemHud.canSelect = false;
            newState = new BattleStatePlayerAwaitAction();
        }

        yield return new WaitForSeconds(0f);
    }
}
