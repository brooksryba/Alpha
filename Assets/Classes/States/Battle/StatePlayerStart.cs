using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerStart : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        _manager.battleSystemHud.disableUnusableHuds(_manager.charManager.attackerName, _manager.charManager.playerParty);
        _manager.battleSystemHud.canSelect = true;

        ToastSystem.instance.Open("Choose an action for "+_manager.charManager.attackerName+":", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        
        if(_manager.battleSystemHud.selection != null && 
           _manager.battleSystemHud.selection.title != _manager.charManager.attackerName) {
            _manager.battleSystemHud.selection = null;
            //newState = new BattleStatePlayerStart();
        }
        
        if(_manager.battleSystemHud.selection != null && 
           _manager.battleSystemHud.selection.title == _manager.charManager.attackerName){
            _manager.battleSystemMenu.OpenSubmenu(_manager.battleSystemHud.selection, _manager.battleSystemHud.selectionButton);
            _manager.battleSystemHud.selection = null;
        } else if(_manager.chosenBattleMove != "" && _manager.chosenBattleMove != null) {
            _manager.battleSystemHud.RefreshAllHUDs();
            animator.SetTrigger("BattleAttack");
            //newState = new BattleStatePlayerAttack();
        } else if(_manager.chosenItem != "" && _manager.chosenItem != null) {
            _manager.battleSystemHud.RefreshAllHUDs();
            animator.SetTrigger("BattleItem");
            //newState = new BattleStateUseItem();
        } else if(
            (_manager.battleSystemHud.selection == null) && 
            ((_manager.chosenBattleMove == "") && (_manager.chosenItem == "")) &&
            (GameObject.Find("Menu(Clone)") == null)) {
               // newState = new BattleStatePlayerStart();
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
