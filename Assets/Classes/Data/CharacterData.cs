using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData {

    public int level;
    public List<ItemData> items;
    public int currentHP;
    public int currentMana;
    public int speed;
    public int earnedXp;

    public List<string> partyMembers;

    public int dialogIndex;


    public CharacterData(Character character) {
        level = character.level;
        items = character.items;
        currentHP = character.currentHP;
        currentMana = character.currentMana;
        speed = character.speed;
        earnedXp = character.earnedXp;
        partyMembers = character.partyMembers;
        dialogIndex = character.dialogIndex;
    }

}
