using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStateAttackAnimationRetreat : BattleState
{
    public BattleMovement battleMovement;
    public override IEnumerator enter()
    {
        battleMovement = _manager.charManager.attacker.GetComponent<BattleMovement>();
        battleMovement.Animate(_manager.charManager.originalPositions[_manager.charManager.attackerName]);
        return base.enter();
    }
    override public IEnumerator execute()
    {
        if(!battleMovement.isAnimating && battleMovement.isFinished) {
            battleMovement.Reset();
            Transition(new BattleStateAttackEnd());
        }

        return base.execute();
    }
}
