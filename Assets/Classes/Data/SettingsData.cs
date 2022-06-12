using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData {
    public float volume;

    public SettingsData(SettingsSystem settings) {
        volume = settings.volume;
    }
}
