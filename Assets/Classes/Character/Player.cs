using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int level = 0;
    public int health = 10;

    void Start()
    {
        LoadState();
    }

    void Update()
    {
        if( Input.GetKeyUp(KeyCode.Q) ) {
            SaveState();
        }
        if( Input.GetKeyUp(KeyCode.R) ) {
            SaveSystem.Reset();
        }        
    }

    public void SaveState()
    {
        SaveSystem.SaveState<PlayerData>(new PlayerData(this), gameObject.name);
    }

    public void LoadState()
    {
        PlayerData data = SaveSystem.LoadState<PlayerData>(gameObject.name) as PlayerData;
        if( data != null ) {
            this.level = data.level;
            this.health = data.health; 

            Vector3 position;
            position.x = data.position[0];
            position.y = data.position[1];
            position.z = 0;

            transform.position = position;
        }
    }
}
