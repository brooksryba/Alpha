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
        ToastSystem.instance.Open("Choose a target for "+_manager.charManager.attackerName+":", false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        BattleSystemUtils battleSystemUtils = new BattleSystemUtils();

        if(_manager.battleSystemHud.selection != null) {
            if(battleSystemUtils.ConfirmBattleMoveInputs(_manager.chosenBattleMove, _manager.charManager.attacker.GetComponent<Character>(), _manager.battleSystemHud.selection)){
                _manager.battleSystemHud.canSelect = false;        
            
                bool isAccepted = battleSystemUtils.ConfirmBattleMoveFeasibility(_manager.chosenBattleMove, _manager.charManager.attacker.GetComponent<Character>(), _manager.battleSystemHud.selection); 

                _manager.battleSystemHud.RefreshAllHUDs();
                if(isAccepted){
                    if(_manager.battleSystemHud.selection)
                        _manager.SetDefender(_manager.battleSystemHud.selection.title);
                    else
                        _manager.SetDefender("");
                    animator.SetTrigger("BattleSelection");
                    _manager.battleSystemHud.selection = null;

                }
                else {
                    ToastSystem.instance.Open(_manager.charManager.attackerName + " cannot choose this attack", false);
                    _manager.battleSystemHud.selection = null;
                    _manager.battleSystemHud.canSelect = true;
                    animator.SetTrigger("BattleInvalidSelection");
                }
            }    
        }
    }
}
