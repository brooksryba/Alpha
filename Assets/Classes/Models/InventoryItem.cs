using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string title;
    public bool active;


    void Start()
    {
        SaveSystem.instance.Register(gameObject.name + transform.GetSiblingIndex(), () => { SaveState(); });
        LoadState();
    }

    public void SaveState()
    {
        SaveSystem.instance.SaveState<ItemData>(new ItemData(this), gameObject.name + transform.GetSiblingIndex());
    }

    public void LoadState()
    {
        ItemData data = SaveSystem.instance.LoadState<ItemData>(gameObject.name + transform.GetSiblingIndex()) as ItemData;
        if( data != null ) {
            gameObject.SetActive(data.active);
        }
    }
}
