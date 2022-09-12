using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneSystem : MonoBehaviour
{
    public bool cutsceneIsPlaying { get; private set; }

    public void EnterCutsceneMode()
    {
        cutsceneIsPlaying = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ExitCutsceneMode()
    {
        cutsceneIsPlaying = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
