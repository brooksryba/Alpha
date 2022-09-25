using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class CameraSystem : MonoBehaviour
{
    public Light2D lightComponent;
    public Color nightColor;

    public void Start() {
        InvokeRepeating("Refresh", 0.0f, 5f);
    }

    public void Refresh() {
        int hour = System.DateTime.Now.Hour;

        float percentOfDay = (float)hour/24f;
        float percentOfPi = 2f * Mathf.PI * percentOfDay;
        float curve = Mathf.Cos(percentOfPi);

        Color colorValue = Color.Lerp(Color.white, nightColor, curve);
        lightComponent.color = colorValue;
    }
}
