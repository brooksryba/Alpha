using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    public Slider slider;
    public SettingsSystem system;

    void Start() {
        system.LoadState();
        slider.value = system.volume;
    }

    public void SetVolume(float input)
    {
        system.SetVolume(input);
        system.SaveState();
    }
}
