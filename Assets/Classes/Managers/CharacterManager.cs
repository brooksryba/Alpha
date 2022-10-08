using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterManager
{
    public static List<Character> refs = new List<Character>(); // TODO - Should this be a list or a dictionary<int, Character>? ideally the id is the index/key
    public static Character Get(int id)
    {
        return new Character();
    }

    public static void SetStats(){
        // TODO - Should this live here? It does seem helpful to have a single function to set the stats
        // for when a player levels up. Logic below is the old code

        // this.speed = GetCurrentStatValue("speed", level);
        // this.physicalAttack = GetCurrentStatValue("physicalAttack", level);
        // this.magicAttack = GetCurrentStatValue("magicAttack", level);
        // this.physicalDefense = GetCurrentStatValue("physicalDefense", level);
        // this.magicDefense = GetCurrentStatValue("magicDefense", level);
        // this.maxHP = GetCurrentStatValue("maxHp", level);
        // this.maxMana = GetCurrentStatValue("maxMana", level);
    }
    
}
