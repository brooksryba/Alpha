using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterManager
{
    public static Dictionary<string, Character> refs;

    
    public static void LoadData(){
        Debug.Log("Calling LoadData");
        Debug.Log(refs);
        refs = CsvReader.ReadCharacterCsv();
        Debug.Log(refs);
    }
    
    public static Character Get(string id)
    {
        Debug.Log("Trying to get: " + id);
        Debug.Log(refs);
        return refs[id];
    }

    public static void SetStats(string id){
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
