using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public Vector3 position;

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
            this.level = data.level;
            this.currentHP = data.currentHP; 
            this.currentMana = data.currentMana; 

            this.position = new Vector3();
            this.position.x = data.position[0];
            this.position.y = data.position[1];
            this.position.z = 0;

            this.items = new List<ItemData>();
            foreach( var item in data.items )
            {
                this.items.Add(item);
            }
        }
    }
}
