using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateGetAttacker : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;

        // initialize the list if this is called the first time a battle starts only, gathers all players once initialized, checks for dead players, sets turn
        if(battleObjManager.turnIndex == -1){
            battleObjManager.allCharacters = new List<Character>();
            foreach(var p in battleObjManager.allPlayers){
                battleObjManager.allCharacters.Add(battleSystemUtils.GetCharacter(p));
            }

            for(int i = 0; i < battleObjManager.allPlayers.Count; i++){
                if(battleSystemUtils.CheckPlayerDeadAndAnimate(battleObjManager.allPlayers[i]))
                    battleObjManager.deadPlayerList.Add(battleObjManager.allPlayers[i]);
            }
            battleObjManager.overallTurnNumber = 0;
            
        }

        //coninually add 1 to turn index until a non-dead play is picked. If we hit the whole list of players, reset the speed list and add to overall turn count
        // It loops through 2*# of players as the speeds might change mid battle. Due to order change, going through size of list twice guarantees selection
        for(int i = 1; i <= 2*battleObjManager.allCharacters.Count; i++){
            battleObjManager.turnIndex = (battleObjManager.turnIndex + 1) % (battleObjManager.allCharacters.Count);
            if(battleObjManager.turnIndex == 0){
                battleObjManager.allCharacters.Sort(delegate(Character a, Character b){return (b.speed).CompareTo(a.speed);}); // highest speed first (a comp to b is lowest)
                battleObjManager.overallTurnNumber += 1;
                battleObjManager.turnCounterText.text = "Overall Turn: " + battleObjManager.overallTurnNumber.ToString();

            }
            if(!battleObjManager.deadPlayerList.Contains(battleObjManager.allCharacters[battleObjManager.turnIndex].title)){
                break;
            }

        }
        
        Character nextUp = battleObjManager.allCharacters[battleObjManager.turnIndex];
        battleObjManager.dialogueText.text = "It is "+nextUp.title+"'s turn to attack!";

        if(battleObjManager.playerParty.Contains(nextUp.title)){
            battleObjManager.playerUnit = nextUp;
            battleObjManager.battleSystemHud.disableUnusableHuds(nextUp.title, battleObjManager.playerParty);
            newState =  new BattleStatePlayerStart();
        } else {
            battleObjManager.enemyUnit = nextUp;
            newState =  new BattleStateEnemyStart();
        }

        yield return new WaitForSeconds(0f);
    }
}
