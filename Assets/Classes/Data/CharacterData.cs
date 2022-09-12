using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData {

    public bool active;
    
    public int level;
    public List<ItemData> items;
    public int currentHP;
    public int currentMana;
    public int earnedXp;

    public List<string> partyMembers;

    public CharacterData(Character character) {
        active = character.gameObject.activeSelf;

        level = character.level;
        items = character.items;
        currentHP = character.currentHP;
        currentMana = character.currentMana;
        earnedXp = character.earnedXp;
        partyMembers = character.partyMembers;
    }

}
