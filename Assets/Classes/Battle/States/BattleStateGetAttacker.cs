using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateGetAttacker : BattleState
{

    
    override public IEnumerator execute()
    {
        // Loops through ordered list of all characters by speed and selects the next in order to attack
        // Potential concerns - Are we allowing speed changes mid-battle, if so how do we handle that? Currently, the order of the list will change, but turn number does not reset
        // hold a variable in object manager for whole turns so that it runs through each character before recalcing speed list
        newState = this;

        battleObjManager.turnIndex += 1;
        int totalPlayers = battleObjManager.playerParty.Count + battleObjManager.enemyParty.Count;
        if(battleObjManager.turnIndex >= totalPlayers)
            battleObjManager.turnIndex = 0;
        List<Character> allCharacters = new List<Character>();
        foreach(var p in battleObjManager.playerParty){
            allCharacters.Add(battleSystemUtils.GetCharacter(p));
        }
        foreach(var p in battleObjManager.enemyParty){
            allCharacters.Add(battleSystemUtils.GetCharacter(p));
        }

        allCharacters.Sort(delegate(Character a, Character b){
            return (b.speed).CompareTo(a.speed); // highest speed first (a comp to b is lowest)
        });

        for(int i = 0; i < allCharacters.Count; i++){
            int indexToCheck = (battleObjManager.turnIndex + i) % allCharacters.Count;
            if(allCharacters[indexToCheck].currentHP > 0){
                battleObjManager.turnIndex = indexToCheck;
                break;
            }
        }
        
        Character nextUp = allCharacters[battleObjManager.turnIndex];
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
