using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateSetup : BattleState
{
    public override IEnumerator enter()
    {
        Toast(_manager.charManager.enemyParty[0] + " engages in battle...");
        return base.enter();
    }

    override public IEnumerator execute()
    {
        Transition(new BattleStateGetAttacker());
        return base.execute(1f);
    }
}
