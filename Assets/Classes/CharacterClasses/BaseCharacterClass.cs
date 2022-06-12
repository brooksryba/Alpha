using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BaseCharacterClass
{
    public string className;
    public Dictionary<string, double> baseStats = new Dictionary<string, double>();
    public Dictionary<string, double> statProgression = new Dictionary<string, double>();

    public int speed;
    public int physicalAttack;
    public int magicAttack;
    public int physicalDefense;
    public int magicDefense;
    public int maxHP;
    public int maxMana;

    public void SetStats(int level){
        this.speed = GetCurrentStatValue("speed", level);
        this.physicalAttack = GetCurrentStatValue("physicalAttack", level);
        this.magicAttack = GetCurrentStatValue("magicAttack", level);
        this.physicalDefense = GetCurrentStatValue("physicalDefense", level);
        this.magicDefense = GetCurrentStatValue("magicDefense", level);
        this.maxHP = GetCurrentStatValue("maxHp", level);
        this.maxMana = GetCurrentStatValue("maxMana", level);
    }
    

    public int GetCurrentStatValue(string statName, int currentLevel){
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
