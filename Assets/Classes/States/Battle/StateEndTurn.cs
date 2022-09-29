using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEndTurn : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        BattleSystemUtils battleSystemUtils = new BattleSystemUtils();

        _manager.battleBonusManager.IncrementPlayerTurn(_manager.charManager.attacker.GetComponent<Character>().title);
        _manager.battleSystemHud.RefreshAllHUDs();
         _manager.battleSystemHud.selection = null;
        _manager.chosenBattleMove = "";

        animator.ResetTrigger("BattleAttack");
        animator.ResetTrigger("BattleItem");
        animator.ResetTrigger("BattleSelection");
        animator.ResetTrigger("BattleMinigame");
        animator.ResetTrigger("BattleApproach");
        animator.ResetTrigger("BattleEffects");
        animator.ResetTrigger("BattleRetreat");
        animator.SetBool("battleOver", battleSystemUtils.PartyDead(_manager.charManager.enemyParty) || battleSystemUtils.PartyDead(_manager.charManager.playerParty));        
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
