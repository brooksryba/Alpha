using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public string title;
    public bool active;


    void Start()
    {
        LoadState();
    }

    public void SaveState()
    {
        SaveSystem.SaveState<ItemData>(new ItemData(this), gameObject.name);
    }

    public void LoadState()
    {
        ItemData data = SaveSystem.LoadState<ItemData>(gameObject.name) as ItemData;
        if( data != null ) {
            gameObject.SetActive(data.active);
        }
    }    
}
