using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    void Start()
    {
        if(SystemInfo.deviceType != DeviceType.Handheld){
            GameObject.Find("JoystickOverlay").SetActive(false);
        }        
    }
}
