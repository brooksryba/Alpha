using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    

    public List<ItemData> items;


    void Start()
    {
        LoadState();
    }

    public void AddInventoryItem(InventoryItem item)
    {
        items.Add(new ItemData(item));
    }

    public void SaveState()
    {
        SaveSystem.SaveState<PlayerData>(new PlayerData(this), "Player");
    }

    public void LoadState()
    {
        PlayerData data = SaveSystem.LoadState<PlayerData>("Player") as PlayerData;
        if( data != null ) {
            this.items = new List<ItemData>();

            foreach( var item in data.items )
            {
                this.items.Add(item);
            }


        }

    }
}
