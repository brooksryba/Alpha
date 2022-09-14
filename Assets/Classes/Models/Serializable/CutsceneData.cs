using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CutsceneData {
    public bool active;

    public CutsceneData(Cutscene cutscene) {
        active = cutscene.gameObject.activeSelf;
    }
}
