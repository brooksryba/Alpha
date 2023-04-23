using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchetypeManager
{
    public static Dictionary<string, Archetype> refs = new Dictionary<string, Archetype>();
    
    public static Archetype Get(string id)
    {
        refs.Add("Player", new Archetype());
        return refs[id];
    }

}
