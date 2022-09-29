using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyAttack : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        EnemyAttackChooser attackChooser = new EnemyAttackChooser();
        string newMessage = "";

        List<string> chosenAttackList = attackChooser.GetAttack(_manager.charManager.attackerName);

        _manager.chosenBattleMove = chosenAttackList[0];

        if(chosenAttackList[1] != "")
            newMessage = _manager.charManager.attackerName + " attacks " + chosenAttackList[1] + " with " + _manager.chosenBattleMove + "!";
        else 
            newMessage = _manager.charManager.attackerName + " uses " + _manager.chosenBattleMove + "!";

        _manager.SetDefender(chosenAttackList[1]);

        if(chosenAttackList[1] != "") _manager.charManager.defender = GameObject.Find(chosenAttackList[1]);
        
        ToastSystem.instance.Open(newMessage, false);       
        animator.SetTrigger("BattleAttack");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
