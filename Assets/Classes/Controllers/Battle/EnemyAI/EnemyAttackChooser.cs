using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAttackChooser
{
    public BattleObjectManager _manager = BattleObjectManager.instance;
    public BattleSystemUtils utils = new BattleSystemUtils();

    public List<int> GetPossibleAttackTargets(){
        List<int> possibleTargets = new List<int>();
        foreach(int characterID in _manager.allCharactersNew){
            (int currentHp, int maxHp) = CharacterManager.Get(characterID).condition.hp;
            if(currentHp > 0){
                possibleTargets.Add(characterID);
            }
        }
        return possibleTargets;
    }



    public Dictionary<string, int> GetAllAttackOptions(int enemyID){
        Dictionary<string, int> allOptions = new Dictionary<string, int>();
        List<Move> allBattleMoves = new List<Move>();
        List<int> allTargets = GetPossibleAttackTargets();

        foreach(Move attack in CharacterManager.Get(enemyID).condition.attacks)
            allBattleMoves.Add(attack);

        foreach(Move spell in CharacterManager.Get(enemyID).condition.spells)
            allBattleMoves.Add(spell);

        // NOTE: this gives only 1 instance of an attack that does not require a target, so scaling might be needed for smarter AI

        // @ TODO needs TLC
        // foreach(Move move in allBattleMoves){
        //     battleMoveRef.userName = enemyName;
        //     if(battleMoveRef.needsTarget){
        //         foreach(var t in allTargets){
        //             battleMoveRef.targetName = t;
        //             if(battleMoveRef.CheckFeasibility()){
        //                 int attackPointsAi = battleMoveRef.GetMoveValueForAi();
        //                 if(attackPointsAi <= 0)
        //                     continue;
        //                 allOptions.Add(a + "|" + t, attackPointsAi);
        //             }
        //         }

        //     } else {
        //         if(battleMoveRef.CheckFeasibility()){
        //             int attackPointsAi = battleMoveRef.GetMoveValueForAi();
        //             if(attackPointsAi <= 0)
        //                 continue;
        //             allOptions.Add(a + "|", attackPointsAi);
        //         }

        //     }

        // }

        return allOptions;
    }

    public (int, Move) GetAttack(string enemyName){
        // string[] attack_target;
        // Dictionary<string, int> attackOptions = GetAllAttackOptions(enemyName);

        // List<string> attackNames = new List<string>(attackOptions.Keys);
        // List<int> pointsAi = new List<int>(attackOptions.Values);
        // pointsAi = changeAiAttackChances(pointsAi);
        // int randomlyChosenAttackIndex = GetRandomWeightedIndex(pointsAi);
        // string chosenAttackKey = attackNames[randomlyChosenAttackIndex];
        // attack_target = chosenAttackKey.Split(char.Parse("|"));

        // return new List<string>(attack_target);

        // @TODO - this code below can be removed, this should return -1 for attacking all
        return (0, new Move());
    }

    public List<int> changeAiAttackChances(List<int> listOfPoints){
        List<int> pointsAi = new List<int>();

        for(int i = 0; i < listOfPoints.Count; i++){
            // Change points here to make more or less difficult
            pointsAi.Add(listOfPoints[i]);
        }

        return pointsAi;
    }



    public int GetRandomWeightedIndex(List<int> weights)
    {
        // Get the total sum of all the weights.
        int weightSum = 0;
        for (int i = 0; i < weights.Count; ++i)
        {
            weightSum += weights[i];
        }
    
        // Step through all the possibilities, one by one, checking to see if each one is selected.
        int index = 0;
        int lastIndex = weights.Count - 1;
        while (index < lastIndex)
        {
            // Do a probability check with a likelihood of weights[index] / weightSum.
            if (UnityEngine.Random.Range(0, weightSum) < weights[index])
            {
                return index;
            }
    
            // Remove the last item from the sum of total untested weights and try again.
            weightSum -= weights[index++];
        }
    
        // No other item was selected, so return very last index.
        return index;
    }



}
