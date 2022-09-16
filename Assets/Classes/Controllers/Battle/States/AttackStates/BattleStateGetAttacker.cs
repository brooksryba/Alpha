using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateGetAttacker : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;

        // initialize the list if this is called the first time a battle starts only, gathers all players once initialized, checks for dead players, sets turn
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
        newMessage = "It is " + _manager.charManager.attackerName + "'s turn to attack!";
        

        if(_manager.charManager.playerParty.Contains(_manager.charManager.attackerName)){
            newState =  new BattleStatePlayerStart();
        } else {
            newState =  new BattleStateEnemyStart();
        }

        if(_manager.battleBonusManager.CheckSkipTurn(_manager.charManager.attackerName)){
            yield return new WaitForSeconds(1.5f);
            newMessage = _manager.charManager.attackerName + " cannot attack this round!";
            newState = new BattleStateAttackEnd();

        }

        yield return new WaitForSeconds(0f);
    }
}
