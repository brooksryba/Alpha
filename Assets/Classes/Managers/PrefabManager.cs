using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefabManager 
{
    public enum Types {
        Character,
        Minigame,
        Item
    }

    public static GameObject Load(Transform parent, string id, Types type) 
    {
        // Instantiate(calc path from enum + int, parent);
        return new GameObject();
    }

    public static GameObject Get(string id, Types type) 
    {
        // Instantiate(calc path from enum + int, parent);
        return new GameObject();
    }

}

