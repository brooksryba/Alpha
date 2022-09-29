using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateStartTurn : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        BattleSystemUtils battleSystemUtils = new BattleSystemUtils();        
        
        if(_manager.turnIndex == -1){
            _manager.charManager.characterTurnOrder = new List<Character>();
            foreach(var p in _manager.charManager.allPlayers){
                _manager.charManager.characterTurnOrder.Add(battleSystemUtils.GetCharacter(p));
            }

            for(int i = 0; i < _manager.charManager.allPlayers.Count; i++){
                if(battleSystemUtils.CheckPlayerDeadAndAnimate(_manager.charManager.allPlayers[i]))
                    _manager.charManager.deadPlayerList.Add(_manager.charManager.allPlayers[i]);
            }
            _manager.overallTurnNumber = 0;
            
        }

        //coninually add 1 to turn index until a non-dead play is picked. If we hit the whole list of players, reset the speed list and add to overall turn count
        // It loops through 2*# of players as the speeds might change mid battle. Due to order change, going through size of list twice guarantees selection
        for(int i = 1; i <= 2*_manager.charManager.characterTurnOrder.Count; i++){
            _manager.turnIndex = (_manager.turnIndex + 1) % (_manager.charManager.characterTurnOrder.Count);
            if(_manager.turnIndex == 0){
                _manager.charManager.characterTurnOrder.Sort(delegate(Character a, Character b){return (b.characterClass.speed).CompareTo(a.characterClass.speed);}); // highest speed first (a comp to b is lowest)
                _manager.overallTurnNumber += 1;

            }
            if(!_manager.charManager.deadPlayerList.Contains(_manager.charManager.characterTurnOrder[_manager.turnIndex].title)){
                break;
            }

        }
        
        Character nextUp = _manager.charManager.characterTurnOrder[_manager.turnIndex];
        _manager.SetAttacker(nextUp.title);
        

        animator.SetBool("battlePlayerTurn", _manager.charManager.playerParty.Contains(_manager.charManager.attackerName));

        ToastSystem.instance.Open("It is " + _manager.charManager.attackerName + "'s turn to attack!", false);
        
        animator.SetBool("battleSkipTurn", false);
        if(_manager.battleBonusManager.CheckSkipTurn(_manager.charManager.attackerName)){
            ToastSystem.instance.Open(_manager.charManager.attackerName + " cannot attack this round!", false);
            animator.SetBool("battleSkipTurn", true);
        }       

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
