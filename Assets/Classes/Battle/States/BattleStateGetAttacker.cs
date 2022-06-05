using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateGetAttacker : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;

        // initialize the list if this is called for first time in a battle
        if(battleObjManager.turnIndex == -1){
            battleObjManager.allCharacters = new List<Character>();
            foreach(var p in battleObjManager.allPlayers){
                battleObjManager.allCharacters.Add(battleSystemUtils.GetCharacter(p));
            }
        }

        // step into next turn, turnover if you've reached the end, re-sort the list if all characters have been cycled through or it's the first turn
        battleObjManager.turnIndex += 1;
        if(battleObjManager.turnIndex >= battleObjManager.allPlayers.Count) battleObjManager.turnIndex = 0;

        if(battleObjManager.turnIndex == 0){
            battleObjManager.allCharacters.Sort(delegate(Character a, Character b){
                return (b.speed).CompareTo(a.speed); // highest speed first (a comp to b is lowest)
            });
        }
        

        for(int i = 0; i < battleObjManager.allCharacters.Count; i++){
            int indexToCheck = (battleObjManager.turnIndex + i) % battleObjManager.allCharacters.Count;
            if(battleObjManager.allCharacters[indexToCheck].currentHP > 0){
                battleObjManager.turnIndex = indexToCheck;
                break;
            }
        }
        
        Character nextUp = battleObjManager.allCharacters[battleObjManager.turnIndex];
        if(battleObjManager.playerParty.Contains(nextUp.title)){
            battleObjManager.dialogueText.text = "It is "+nextUp.title+"'s turn to attack!";
            battleObjManager.playerUnit = nextUp;
            battleObjManager.battleSystemHud.disableUnusableHuds(nextUp.title, battleObjManager.playerParty);
            newState =  new BattleStatePlayerStart();
        } else {
            battleObjManager.dialogueText.text = "It is "+nextUp.title+"'s turn to attack!";
            battleObjManager.enemyUnit = nextUp;
            newState =  new BattleStateEnemyStart();
        }

        yield return new WaitForSeconds(0f);
    }
}
