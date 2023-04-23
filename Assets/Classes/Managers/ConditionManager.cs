using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionManager
{
    public static Dictionary<string, Condition> refs = new Dictionary<string, Condition>();
    
    public static Condition Get(string id)
    {
        refs.Add("Player", new Condition());
        return refs[id];
    }

    
}
