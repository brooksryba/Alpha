using UnityEngine;
using UnityEngine.Audio;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class SettingsSystem : MonoBehaviour {
    public float volume; 
    public AudioMixer audioMixer;

    public void Start() {
        LoadState();
    }

    public void SaveState()
    {
        SaveSystem.SaveState<SettingsData>(new SettingsData(this), "SettingsData");
    }

    public void LoadState()
    {
        SettingsData data = SaveSystem.LoadState<SettingsData>("SettingsData") as SettingsData;

        if (data != null)
        {
            SetVolume(data.volume);
        }
    }

    public void SetVolume(float input) {
        volume = input;
        audioMixer.SetFloat("volume", volume);
    }
}