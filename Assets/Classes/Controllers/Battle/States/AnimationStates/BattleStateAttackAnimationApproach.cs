using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStateAttackAnimationApproach : BattleState
{
    public BattleMovement battleMovement;

    public override IEnumerator enter()
    {
        bool isPlayer = (_manager.charManager.playerParty.Contains(_manager.charManager.attackerName));

        battleMovement = _manager.charManager.attacker.GetComponent<BattleMovement>();

        Vector3 offset = new Vector3(isPlayer ? 2 : -2, 0, 0);
        Vector3 targetPosition = new Vector3();
        if(_manager.charManager.defender && _manager.chosenMoveDetails.moveType != "Spell") {
            targetPosition = _manager.charManager.defender.transform.position;
        }
        else {
            targetPosition = offset + new Vector3(0, 2, 0);
        }
        battleMovement.Animate(targetPosition - offset); 
        // @todo - The subtraction in the above line is not a fix and should be fixed

        return base.enter();
    }
    override public IEnumerator execute()
    {
        if(!battleMovement.isAnimating && battleMovement.isFinished) {
            battleMovement.Reset();
            Transition(new BattleStateAttackAnimationEffects());
        }

        return base.execute();
    }
}
