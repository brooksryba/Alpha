using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerAttack : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        _manager.battleSystemMenu.closeOptionSubmenu();
        _manager.battleSystemHud.canSelect = true;
        ToastSystem.instance.Open("Choose a target for "+_manager.condition.attackerName+":", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        BattleSystemUtils battleSystemUtils = new BattleSystemUtils();

        if(_manager.battleSystemHud.selection != null) {
            if(battleSystemUtils.ConfirmBattleMoveInputs(_manager.chosenMove, CharacterManager.Get(_manager.condition.attackerName), CharacterManager.Get(_manager.battleSystemHud.selection.characterID))){
                _manager.battleSystemHud.canSelect = false;        
            
                bool isAccepted = battleSystemUtils.ConfirmBattleMoveFeasibility(_manager.chosenMove, CharacterManager.Get(_manager.condition.attackerName), CharacterManager.Get(_manager.battleSystemHud.selection.characterID)); 

                _manager.battleSystemHud.RefreshAllHUDs();
                if(isAccepted){
                    if(_manager.battleSystemHud.selection)
                        _manager.SetDefender(_manager.battleSystemHud.selection.characterID);
                    else
                        _manager.SetDefender("");
                    animator.SetTrigger("BattleSelection");
                    _manager.battleSystemHud.selection = null;

                }
                else {
                    ToastSystem.instance.Open(_manager.condition.attackerName + " cannot choose this attack", false);
                    _manager.battleSystemHud.selection = null;
                    _manager.battleSystemHud.canSelect = true;
                    animator.SetTrigger("BattleInvalidSelection");
                }
            }    
        }
    }
}
