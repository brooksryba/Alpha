using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleBonus
{
    public string playerName;
    public string statName;
    public double statMultiplier;
    public double statAddition;
    public int bonusDuration;

    public void SetBonusProperties(string playerName, string statName, double statMultiplier, double statAddition, int bonusDuration){
        this.playerName = playerName;
        this.statName = statName;
        this.statMultiplier = statMultiplier;
        this.statAddition = statAddition;
        this.bonusDuration = bonusDuration;

    }

    // void Init(){
    //     playerStatIndex.Add("physicalAttack");
    //     playerStatIndex.Add("physicalDefense");
    //     playerStatIndex.Add("magicAttack");
    //     playerStatIndex.Add("magicDefense");
    //     playerStatIndex.Add("speed");

    //     for(int i = 0; i < playerStatIndex.Count; i++){
    //         bonusMultipliers.Add(1.0);
    //         bonusAdditions.Add(0.0);
    //     }
    // }

    // public void ResetBattleStat(string statName){
    //     int index = playerStatIndex.IndexOf(statName);
    //     bonusMultipliers[index] = 1.0;
    //     bonusAdditions[index] = 0.0;

    // }

    // public int GetBattleStat(string statName, bool applyBonus=true)
    // {
    //     int index = playerStatIndex.IndexOf(statName);
    //     if(applyBonus)
    //         return (int)(GetCharacter(playerName).physicalAttack*bonusMultipliers[index] + bonusAdditions[index]);          
    //     return GetCharacter(playerName).physicalAttack;
    // }



    // public Character GetCharacter(string id)
    // {
    //     return GameObject.Find(id).GetComponent<Character>();
    // }


}
