using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateEnemyStart : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;

        newMessage = _manager.charManager.attackerName + " is about to attack!";
        newState = new BattleStateEnemyAttack();
        yield return new WaitForSeconds(2f);
    }
}
