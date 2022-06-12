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
            _manager.characterTurnOrder = new List<Character>();
            foreach(var p in _manager.allPlayers){
                _manager.characterTurnOrder.Add(battleSystemUtils.GetCharacter(p));
            }

            for(int i = 0; i < _manager.allPlayers.Count; i++){
                if(battleSystemUtils.CheckPlayerDeadAndAnimate(_manager.allPlayers[i]))
                    _manager.deadPlayerList.Add(_manager.allPlayers[i]);
            }
            _manager.overallTurnNumber = 0;
            
        }

        //coninually add 1 to turn index until a non-dead play is picked. If we hit the whole list of players, reset the speed list and add to overall turn count
        // It loops through 2*# of players as the speeds might change mid battle. Due to order change, going through size of list twice guarantees selection
        for(int i = 1; i <= 2*_manager.characterTurnOrder.Count; i++){
            _manager.turnIndex = (_manager.turnIndex + 1) % (_manager.characterTurnOrder.Count);
            if(_manager.turnIndex == 0){
                _manager.characterTurnOrder.Sort(delegate(Character a, Character b){return (b.characterClass.speed).CompareTo(a.characterClass.speed);}); // highest speed first (a comp to b is lowest)
                _manager.overallTurnNumber += 1;
                _manager.turnCounterText.text = "Overall Turn: " + _manager.overallTurnNumber.ToString();

            }
            if(!_manager.deadPlayerList.Contains(_manager.characterTurnOrder[_manager.turnIndex].title)){
                break;
            }

        }
        
        Character nextUp = _manager.characterTurnOrder[_manager.turnIndex];
        _manager.SetAttacker(nextUp.title);
        _manager.dialogueText.text = "It is " + _manager.attackerName + "'s turn to attack!";
        

        if(_manager.playerParty.Contains(_manager.attackerName)){
            newState =  new BattleStatePlayerStart();
        } else {
            newState =  new BattleStateEnemyStart();
        }

        if(_manager.battleBonusManager.CheckSkipTurn(_manager.attackerName)){
            yield return new WaitForSeconds(1.5f);
            _manager.dialogueText.text = _manager.attackerName + " cannot attack this round!";
            newState = new BattleStateAttackEnd();

        }

        yield return new WaitForSeconds(0f);
    }
}
