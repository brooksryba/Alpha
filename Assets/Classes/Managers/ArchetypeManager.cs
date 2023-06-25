using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchetypeManager
{
    public static Dictionary<string, Archetype> refs = new Dictionary<string, Archetype>();

    public static void LoadData(){
        refs = CsvReader.ReadArchetypeCsv();
    }
    public static Archetype Get(string id)
    {
        return refs[id];
    }

}
