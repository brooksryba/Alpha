using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PrefabManager
{

    private static string baseDir = "Prefabs/";
    public enum Types {
        Character,
        Minigame,
        Item
    }

    public static string GetPathFromType(Types type){
        string subDirectory = "";
        if (type == Types.Character){
            subDirectory = "Characters";
        } else if (type == Types.Minigame){
            subDirectory = "Minigames";
        } else if (type == Types.Item){
            subDirectory = "Items";
        }
        return baseDir + subDirectory + "/";
    }

    public static GameObject Load(Transform parent, string prefabName, Types type) 
    {
        // Instantiate(calc path from enum + int, parent);
        string prefabPath = GetPathFromType(type);
        GameObject loadedPrefab = Resources.Load<GameObject>(prefabPath + prefabName);
        return Object.Instantiate(loadedPrefab, parent);
    }

    public static GameObject Get(string id, Types type) 
    {
        // Instantiate(calc path from enum + int, parent);
        return new GameObject();
    }

}

