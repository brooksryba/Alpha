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
        
        if(_manager.condition.turnIndex == -1){
            _manager.condition.characterTurnOrder = new List<Character>();
            foreach(var p in _manager.condition.allPlayers){
                _manager.condition.characterTurnOrder.Add(battleSystemUtils.GetCharacter(p));
            }

            for(int i = 0; i < _manager.condition.allPlayers.Count; i++){
                if(battleSystemUtils.CheckPlayerDeadAndAnimate(_manager.condition.allPlayers[i]))
                    _manager.condition.deadPlayerList.Add(_manager.condition.allPlayers[i]);
            }
            _manager.condition.overallTurnNumber = 0;
            
        }

        //coninually add 1 to turn index until a non-dead play is picked. If we hit the whole list of players, reset the speed list and add to overall turn count
        // It loops through 2*# of players as the speeds might change mid battle. Due to order change, going through size of list twice guarantees selection
        for(int i = 1; i <= 2*_manager.condition.characterTurnOrder.Count; i++){
            _manager.condition.turnIndex = (_manager.condition.turnIndex + 1) % (_manager.condition.characterTurnOrder.Count);
            if(_manager.condition.turnIndex == 0){
                _manager.condition.characterTurnOrder.Sort(delegate(Character a, Character b){return (b.archetype.speed).CompareTo(a.archetype.speed);}); // highest speed first (a comp to b is lowest)
                _manager.condition.overallTurnNumber += 1;

            }
            if(!_manager.condition.deadPlayerList.Contains(_manager.condition.characterTurnOrder[_manager.condition.turnIndex].title)){
                break;
            }

        }
        
        Character nextUp = _manager.condition.characterTurnOrder[_manager.condition.turnIndex];
        _manager.SetAttacker(nextUp.title);
        

        animator.SetBool("battlePlayerTurn", _manager.condition.playerParty.Contains(_manager.condition.attackerName));

        ToastSystem.instance.Open("It is " + _manager.condition.attackerName + "'s turn to attack!", false);
        
        animator.SetBool("battleSkipTurn", false);

        // @TODO - need to figure out how to handle battle status effects
        // if(_manager.battleBonusManager.CheckSkipTurn(_manager.condition.attackerName)){
        //     ToastSystem.instance.Open(_manager.condition.attackerName + " cannot attack this round!", false);
        //     animator.SetBool("battleSkipTurn", true);
        // }       

    }
}
