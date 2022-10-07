using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateApproach : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        bool isPlayer = (_manager.condition.playerParty.Contains(_manager.condition.attackerID));

        BattleMovement battleMovement = _manager.condition.attacker.GetComponent<BattleMovement>();
        if(battleMovement && !battleMovement.isFinished && !battleMovement.isAnimating) {
            Vector3 offset = new Vector3(isPlayer ? 2 : -2, 0, 0);
            Vector3 targetPosition = new Vector3();
            if(_manager.condition.defender && _manager.chosenMove.type != Move.Type.Spell) {
                targetPosition = _manager.condition.defender.transform.position;
            }
            else {
                targetPosition = offset + new Vector3(0, 2, 0);
            }
            battleMovement.Animate(targetPosition - offset); 
            // @todo - The subtraction in the above line is not a fix and should be fixed
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        BattleMovement battleMovement = _manager.condition.attacker.GetComponent<BattleMovement>();        
        if(battleMovement == null || (!battleMovement.isAnimating && battleMovement.isFinished)) {
            if(battleMovement) battleMovement.Reset();
            animator.SetTrigger("BattleApproach");
        }            
    }
}
