using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Systems : MonoBehaviour
{
    private static Systems _instance;
    public static Systems instance { get { return _instance; } }
    private GameObject stateSystem;

    public void Awake()
    {
        _instance = this;
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
