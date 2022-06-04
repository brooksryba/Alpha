using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyAttackChooser
{
    public BattleObjectManager battleObjManager = GameObject.Find("BattleObjectManager").GetComponent<BattleObjectManager>();
    public BattleSystemUtils utils = new BattleSystemUtils();
    public Attack attackLibrary = new Attack();

    public List<string> GetPossibleAttackTargets(){
        List<string> possibleTargets = new List<string>();
        foreach(var pm in battleObjManager.playerParty){
            if(utils.GetCharacter(pm).currentHP > 0){
                possibleTargets.Add(pm);
            }
        }
        return possibleTargets;
    }



    public Dictionary<string, int> GetAllAttackOptions(string enemyName){
        Dictionary<string, int> attackOptions = new Dictionary<string, int>();
        foreach(var t in GetPossibleAttackTargets()){
            foreach(var a in utils.GetCharacter(enemyName).attackNames){
                Attack attackRef = attackLibrary.GetAttackClass(a);
                attackRef.attackerName = enemyName;
                attackRef.defenderName = t;
                if(attackRef.CheckAttackFeasible()){
                    int attackPointsAi = attackRef.GetAttackDamage();
                    if(attackPointsAi <= 0)
                        continue;
                    if(t == "Forest Mage")
                        attackPointsAi = attackPointsAi + 25;
                    attackOptions.Add(a + "|" + t, attackPointsAi);
                }
            }
        }

        return attackOptions; 
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

    public List<int> changeAiAttackChances(List<int> listOfPoints){
        List<int> pointsAi = new List<int>();

        for(int i = 0; i < listOfPoints.Count; i++){
            // Change points here to make more or less difficult
            pointsAi.Add(listOfPoints[i]);
        }

        return pointsAi;
    }


}
