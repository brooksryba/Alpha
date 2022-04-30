using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData {

    public int level;
    public int currentHP;
    public int currentMana;
    public int speed;
    public int earnedXp;


    public CharacterData(Character character) {
        level = character.level;
        currentHP = character.currentHP;
        currentMana = character.currentMana;
        speed = character.speed;
        earnedXp = character.earnedXp;

    }

}
