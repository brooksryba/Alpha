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

    public BattleBonus(string playerName, string statName, double statMultiplier, double statAddition, int bonusDuration){
        this.playerName = playerName;
        this.statName = statName;
        this.statMultiplier = statMultiplier;
        this.statAddition = statAddition;
        this.bonusDuration = bonusDuration;

    }

    public void BattleBonusAction(){
        // @todo - this should probably be handled elsewhere, but could not find ideal location
        if(statName == "currentHp"){
            Character currentPlayer = GameObject.Find(playerName).GetComponent<Character>();
            currentPlayer.multiplyHP(statMultiplier);
            if(statAddition > 0){
                currentPlayer.Heal((int)statAddition);
            } else {
                currentPlayer.TakeDamage((int)(-1.0*statAddition));
            }
            
        }
    }



}
