using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleBonusManager
{
    public List<BattleBonus> battleBonuses = new List<BattleBonus>();

    public void AddBonus(string playerName, string statName, double statMultiplier, double statAddition, int bonusDuration){
        BattleBonus newBonus = new BattleBonus();
        newBonus.SetBonusProperties(playerName, statName, statMultiplier, statAddition, bonusDuration);
        battleBonuses.Add(newBonus);
    }

    public void IncrementPlayerTurn(string playerName){
        for(int i = 0; i < battleBonuses.Count; i++){
            BattleBonus checkBonus = battleBonuses[i];
            if(checkBonus.playerName==playerName){
                battleBonuses[i].bonusDuration -= 1;
                Debug.Log("Just removed turn from bonus " + playerName + "| stat " + battleBonuses[i].statName + "| turns left " + battleBonuses[i].bonusDuration.ToString());
                if(battleBonuses[i].bonusDuration==0){
                    battleBonuses.RemoveAt(i);
                }
            }
        }
    }

    public void DestroyAllBonuses(){
        battleBonuses = new List<BattleBonus>();
    }


    public int GetBattleStat(string playerName, string statName, int initialValue, bool applyBonus=true)
    {
        if(!applyBonus)
            return initialValue;
        double multiplier = 1.0;
        double addition = 0.0;
        for(int i = 0; i < battleBonuses.Count; i++){
            BattleBonus checkBonus = battleBonuses[i];
            if(checkBonus.playerName==playerName && checkBonus.statName == statName && checkBonus.bonusDuration > 0){
                multiplier = multiplier*battleBonuses[i].statMultiplier;
                addition += battleBonuses[i].statAddition;
            }
        }
        return (int)(initialValue*multiplier + addition);
    }


}
