using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerItem : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        _manager.battleSystemMenu.closeOptionSubmenu();
        _manager.battleSystemHud.canSelect = true;            
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        if(BattleItems.lookup.ContainsKey(_manager.chosenItem)) {
            InventoryItemData itemData = BattleItems.lookup[_manager.chosenItem];
            Character activeCharacter = _manager.condition.attacker.GetComponent<Character>();
            ToastSystem.instance.Open(itemData.Execute(activeCharacter), false);
            animator.SetTrigger("BattleSelection");
        } else {
            ToastSystem.instance.Open(_manager.chosenItem + " can't be used now!", false);
            animator.SetTrigger("BattleInvalidSelection");
        }        
        
        _manager.chosenItem = null;          
    }
}
