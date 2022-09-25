using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateEnemyStart : BattleState
{
    public override IEnumerator enter()
    {
        Toast(_manager.charManager.attackerName + " is about to attack!");
        return base.enter(2f);
    }
    override public IEnumerator execute()
    {
        Transition(new BattleStateEnemyAttack());
        return base.execute();
    }
}
