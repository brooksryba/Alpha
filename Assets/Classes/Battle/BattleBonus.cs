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



}
