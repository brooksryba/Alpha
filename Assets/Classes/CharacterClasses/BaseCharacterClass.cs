using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCharacterClass
{
    public string className;
    public Dictionary<string, double> baseStats = new Dictionary<string, double>();
    public Dictionary<string, double> statProgression = new Dictionary<string, double>();
    

    public int getCurrentStatValue(string statName, int currentLevel){
        return (int)(baseStats[statName] + currentLevel*statProgression[statName]) ;
    }

    static public BaseCharacterClass GetCharacterClass(string className){
        switch(className){
            case "Jack":
                return new Jack();
            case "Warrior":
                return new Warrior();
            case "Mage":
                return new Mage();
            case "Archer":
                return new Archer();
            default:
                return new Jack();
        }
    }
}
