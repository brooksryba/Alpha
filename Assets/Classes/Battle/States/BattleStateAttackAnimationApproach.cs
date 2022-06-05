using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStateAttackAnimationApproach : BattleState
{

    override public IEnumerator execute()
    {
        newState = this;
        bool isPlayer = (battleObjManager.playerParty.Contains(battleObjManager.attacker.GetComponent<Character>().title));


        BattleMovement battleMovement = battleObjManager.attacker.GetComponent<BattleMovement>();
        if(battleMovement && !battleMovement.isFinished && !battleMovement.isAnimating) {
            Vector3 offset = new Vector3(isPlayer ? 2 : -2, 0, 0);
            battleObjManager.attackerPositionStart = battleObjManager.attacker.transform.position;
            if(battleObjManager.defender) {
                battleObjManager.defenderPositionStart = battleObjManager.defender.transform.position;
            }
            else {
                battleObjManager.defenderPositionStart = offset + new Vector3(0, 2, 0);
            }
            battleMovement.Animate(battleObjManager.attacker.transform.position, battleObjManager.defenderPositionStart - offset); 
            // @todo - The subtraction in the above line is not a fix and should be fixed
        }


        if(battleMovement == null || (!battleMovement.isAnimating && battleMovement.isFinished)) {
            if(battleMovement) battleMovement.Reset();
            newState = new BattleStateAttackAnimationEffects();
            
            yield return newState;
        }
    }
}
