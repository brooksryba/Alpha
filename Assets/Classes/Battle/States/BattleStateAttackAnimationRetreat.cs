using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStateAttackAnimationRetreat : BattleState
{

    override public IEnumerator execute()
    {
        newState = this;

        BattleMovement battleMovement = battleObjManager.attacker.GetComponent<BattleMovement>();
        if(battleMovement && !battleMovement.isFinished && !battleMovement.isAnimating) {
            battleMovement.Animate(battleObjManager.attacker.transform.position, battleObjManager.attackerPositionStart);
        }

        if(battleMovement == null || (!battleMovement.isAnimating && battleMovement.isFinished)) {
            if(battleMovement) battleMovement.Reset();
            newState = new BattleStateAttackEnd();
            yield return newState;
        }
    }
}
