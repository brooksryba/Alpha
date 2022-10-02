using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    public Slider sliderMaster;
    public Slider sliderMusic;
    public Slider sliderFX;
    public SettingsSystem system;

    void Start() {
        system.LoadState();
        sliderMaster.value = system.volumeMaster;
        sliderMusic.value = system.volumeMusic;
        sliderFX.value = system.volumeFX;
    }

    public void SetVolumeMaster(float input)
    {
        system.SetVolume(input, SettingsSystem.mixer.Master);
        system.SaveState();
    }

    public void SetVolumeMusic(float input)
    {
        system.SetVolume(input, SettingsSystem.mixer.Music);
        system.SaveState();
    }

    public void SetVolumeFX(float input)
    {
        system.SetVolume(input, SettingsSystem.mixer.FX);
        system.SaveState();
    }
}
