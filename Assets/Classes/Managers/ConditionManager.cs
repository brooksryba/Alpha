using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionManager
{
    public static Dictionary<string, Condition> refs = new Dictionary<string, Condition>();


    
    public static Condition Get(string id)
    {
        if(!refs.ContainsKey("Hero")){
            refs.Add("Hero", ScriptableObject.CreateInstance<Condition>());
            refs.Add("AF", ScriptableObject.CreateInstance<Condition>());
            refs.Add("MF", ScriptableObject.CreateInstance<Condition>());
            refs.Add("Livar", ScriptableObject.CreateInstance<Condition>());
            refs.Add("Stormy", ScriptableObject.CreateInstance<Condition>());
            refs.Add("Murray", ScriptableObject.CreateInstance<Condition>());
        }
            return refs[id];
    }

    
}
