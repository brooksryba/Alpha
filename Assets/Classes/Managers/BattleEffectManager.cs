using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEffectManager
{
    public static Dictionary<string, BattleEffect> refs;

    public static void LoadData(){
        refs = CsvReader.ReadBattleEffectsCsv();
    }
    
    public static BattleEffect Get(string id)
    {
        if(refs == null){
            LoadData();
        }
        return refs[id];
    }
}
