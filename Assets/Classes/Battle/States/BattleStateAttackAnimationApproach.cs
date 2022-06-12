using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStateAttackAnimationApproach : BattleState
{

    override public IEnumerator execute()
    {
        newState = this;
        bool isPlayer = (_manager.playerParty.Contains(_manager.attackerName));


        BattleMovement battleMovement = _manager.attacker.GetComponent<BattleMovement>();
        if(battleMovement && !battleMovement.isFinished && !battleMovement.isAnimating) {
            Vector3 offset = new Vector3(isPlayer ? 2 : -2, 0, 0);
            Vector3 targetPosition = new Vector3();
            if(_manager.defender) {
                targetPosition = _manager.defender.transform.position;
            }
            else {
                targetPosition = offset + new Vector3(0, 2, 0);
            }
            battleMovement.Animate(_manager.attacker.transform.position, targetPosition - offset); 
            // @todo - The subtraction in the above line is not a fix and should be fixed
        }


        if(battleMovement == null || (!battleMovement.isAnimating && battleMovement.isFinished)) {
            if(battleMovement) battleMovement.Reset();
            newState = new BattleStateAttackAnimationEffects();
            
            yield return newState;
        }
    }
}
