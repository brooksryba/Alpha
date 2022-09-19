using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour
{
    [Header("Ink JSON")]
    [SerializeField] public TextAsset inkJSON;
        
    void Start()
    {
        SaveSystem.instance.Register(gameObject.name + transform.GetSiblingIndex(), () => { SaveState(); });
        LoadState();
    }

    public void SaveState()
    {
        SaveSystem.instance.SaveState<CutsceneData>(new CutsceneData(this), gameObject.name + transform.GetSiblingIndex());
    }

    public void LoadState()
    {
        CutsceneData data = SaveSystem.instance.LoadState<CutsceneData>(gameObject.name + transform.GetSiblingIndex()) as CutsceneData;
        if( data != null ) {
            gameObject.SetActive(data.active);
        }
    }
}
