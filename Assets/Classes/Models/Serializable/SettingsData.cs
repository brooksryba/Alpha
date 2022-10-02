using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData {
    public float volumeMaster;
    public float volumeMusic;
    public float volumeFX;

    public SettingsData(SettingsSystem settings) {
        volumeMaster = settings.volumeMaster;
        volumeMusic = settings.volumeMusic;
        volumeFX = settings.volumeFX;
    }
}
