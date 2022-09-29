using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateRetreat : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        BattleMovement battleMovement = _manager.charManager.attacker.GetComponent<BattleMovement>();
        if(battleMovement && !battleMovement.isFinished && !battleMovement.isAnimating) {
            battleMovement.Animate(_manager.charManager.originalPositions[_manager.charManager.attackerName]);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        BattleMovement battleMovement = _manager.charManager.attacker.GetComponent<BattleMovement>();
        if(battleMovement == null || (!battleMovement.isAnimating && battleMovement.isFinished)) {
            if(battleMovement) battleMovement.Reset();
            animator.SetTrigger("BattleRetreat");
        }            
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
