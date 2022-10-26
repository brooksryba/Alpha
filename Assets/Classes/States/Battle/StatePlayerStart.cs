using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerStart : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        _manager.battleSystemHud.disableUnusableHuds(_manager.condition.attackerName, _manager.condition.playerParty);
        _manager.battleSystemHud.canSelect = true;

        ToastSystem.instance.Open("Choose an action for "+_manager.condition.attackerName+":", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;

        if(_manager.battleSystemHud.selection != null && 
           _manager.battleSystemHud.selection.title == _manager.condition.attackerName){
            _manager.battleSystemMenu.OpenSubmenu(_manager.battleSystemHud.selection, _manager.battleSystemHud.selectionButton);
            _manager.battleSystemHud.selection = null;
        } else if(_manager.chosenMove != null) {
            _manager.battleSystemHud.RefreshAllHUDs();
            animator.SetTrigger("BattleAttack");
        } else if(_manager.chosenItem != null) {
            _manager.battleSystemHud.RefreshAllHUDs();
            animator.SetTrigger("BattleItem");
        } else if(_manager.playerResigned == true) {
            _manager.battleSystemHud.RefreshAllHUDs();
            animator.SetTrigger("BattleResign");
        }
    }
}
