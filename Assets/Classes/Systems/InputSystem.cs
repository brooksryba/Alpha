using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public GameObject overlayObject;
    void Start()
    {
        if(SystemInfo.deviceType != DeviceType.Handheld){
            overlayObject.SetActive(false);
        }        
    }
}
