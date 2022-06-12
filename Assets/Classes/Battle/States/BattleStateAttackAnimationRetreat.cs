using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStateAttackAnimationRetreat : BattleState
{

    override public IEnumerator execute()
    {
        newState = this;

        BattleMovement battleMovement = _manager.attacker.GetComponent<BattleMovement>();
        if(battleMovement && !battleMovement.isFinished && !battleMovement.isAnimating) {
            battleMovement.Animate(_manager.attacker.transform.position, _manager.originalPositions[_manager.attackerName]);
        }

        if(battleMovement == null || (!battleMovement.isAnimating && battleMovement.isFinished)) {
            if(battleMovement) battleMovement.Reset();
            newState = new BattleStateAttackEnd();
            yield return newState;
        }
    }
}
