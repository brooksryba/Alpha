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
            if(data.level!=0)
                this.level = data.level;   
            if(data.currentHP!=0)
                this.currentHP = data.currentHP;        
            if(data.currentMana!=0)
                this.currentMana = data.currentMana;
            
            this.maxHP = this.level * 10;
            this.maxMana = this.level * 5;

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
