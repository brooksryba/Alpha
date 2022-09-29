using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Systems : MonoBehaviour
{
    public void Awake()
    {
        if(GameObject.Find("StateSystem") == null) {
            GameObject prefab = Resources.Load("Prefabs/Systems/StateSystem") as GameObject;
            GameObject stateSystem = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
            stateSystem.name = "StateSystem";
        }
    }
}
