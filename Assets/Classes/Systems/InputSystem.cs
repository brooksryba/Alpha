using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystem : MonoBehaviour
{
    public static InputSystem instance { get; private set; }
    public void OnEnable() { instance = this; }

    private bool useOverlay = true;
    public GameObject overlayObject;

    void Start()
    {
        if(SystemInfo.deviceType != DeviceType.Handheld){
            useOverlay = false;
            overlayObject.SetActive(false);
        }        
    }

    public void DisableControls()
    {
        if(useOverlay && overlayObject != null) {
            overlayObject.SetActive(false);
        }
    }

    public void EnableControls()
    {
        if(useOverlay && overlayObject != null) {
            overlayObject.SetActive(true);
        }
    }
}
