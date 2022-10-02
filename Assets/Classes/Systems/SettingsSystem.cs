using UnityEngine;
using UnityEngine.Audio;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class SettingsSystem : MonoBehaviour {
    public static SettingsSystem instance { get; private set; }
    private void OnEnable() { instance = this; }

    public float volumeMaster; 
    public float volumeMusic; 
    public float volumeFX; 
    public AudioMixer _mixer;
    public enum mixer {
        Master,
        Music,
        FX
    };

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
            SetVolume(data.volumeMaster, mixer.Master);
            SetVolume(data.volumeMusic, mixer.Music);
            SetVolume(data.volumeFX, mixer.FX);
        }
    }

    public void SetVolume(float input, SettingsSystem.mixer mixerEnum) {
        switch(mixerEnum) {
            case mixer.Master:
                volumeMaster = input;
                _mixer.SetFloat("volumeMaster", volumeMaster);            
                return;
            case mixer.Music:
                volumeMusic = input;
                _mixer.SetFloat("volumeMusic", volumeMusic);
                return;
            case mixer.FX:
                volumeFX = input;
                _mixer.SetFloat("volumeFX", volumeFX);
                return;
        }
    }
}