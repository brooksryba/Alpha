using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAttackChooser
{
    public BattleObjectManager _manager = GameObject.Find("BattleObjectManager").GetComponent<BattleObjectManager>();
    public BattleSystemUtils utils = new BattleSystemUtils();

    public List<string> GetPossibleAttackTargets(){
        List<string> possibleTargets = new List<string>();
        foreach(var pm in _manager.charManager.allPlayers){
            if(utils.GetCharacter(pm).currentHP > 0){
                possibleTargets.Add(pm);
            }
        }
        return possibleTargets;
    }



    public Dictionary<string, int> GetAllAttackOptions(string enemyName){
        Dictionary<string, int> allOptions = new Dictionary<string, int>();
        List<string> allBattleMoves = new List<string>();
        List<string> allTargets = GetPossibleAttackTargets();

        foreach(var attack in utils.GetCharacter(enemyName).attackNames)
            allBattleMoves.Add(attack);

        foreach(var spell in utils.GetCharacter(enemyName).spellNames)
            allBattleMoves.Add(spell);

        // NOTE: this gives only 1 instance of an attack that does not require a target, so scaling might be needed for smarter AI

        foreach(var a in allBattleMoves){
            BattleMoveBase battleMoveRef = BattleMoveBase.GetBattleMoveClass(a);
            battleMoveRef.userName = enemyName;
            if(battleMoveRef.needsTarget){
                foreach(var t in allTargets){
                    battleMoveRef.targetName = t;
                    if(battleMoveRef.CheckFeasibility()){
                        int attackPointsAi = battleMoveRef.GetMoveValueForAi();
                        if(attackPointsAi <= 0)
                            continue;
                        allOptions.Add(a + "|" + t, attackPointsAi);
                    }
                }

            } else {
                if(battleMoveRef.CheckFeasibility()){
                    int attackPointsAi = battleMoveRef.GetMoveValueForAi();
                    if(attackPointsAi <= 0)
                        continue;
                    allOptions.Add(a + "|", attackPointsAi);
                }

            }

        }

        return allOptions;
    }

    public List<string> GetAttack(string enemyName){
        string[] attack_target;
        Dictionary<string, int> attackOptions = GetAllAttackOptions(enemyName);

        List<string> attackNames = new List<string>(attackOptions.Keys);
        List<int> pointsAi = new List<int>(attackOptions.Values);
        pointsAi = changeAiAttackChances(pointsAi);
        int randomlyChosenAttackIndex = GetRandomWeightedIndex(pointsAi);
        string chosenAttackKey = attackNames[randomlyChosenAttackIndex];
        attack_target = chosenAttackKey.Split(char.Parse("|"));

        return new List<string>(attack_target);
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
