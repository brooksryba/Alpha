using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Systems : MonoBehaviour
{
    public static Systems instance { get; private set; }
    public void OnEnable() { instance = this; }
    
    private GameObject stateSystem;

    public void Awake()
    {
        if(GameObject.Find("StateSystem") == null) {
            Initialize();
        } else {
            stateSystem = GameObject.Find("StateSystem");
        }

        if(stateSystem.GetComponent<StateSystem>().reset == true) {
            Deinitialize();
            Initialize();
        }
    }

    public void Initialize()
    {
        GameObject prefab = Resources.Load("Prefabs/Systems/StateSystem") as GameObject;
        stateSystem = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
        stateSystem.name = "StateSystem";
    }

    public void Deinitialize()
    {
        Destroy(stateSystem);
    }
}
