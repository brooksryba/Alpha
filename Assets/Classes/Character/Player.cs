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
    }

    void SaveState()
    {
        Debug.Log("Saving");
        SaveSystem.SaveStatePlayer(this);
    }

    void LoadState()
    {
        Debug.Log("Loading");
        PlayerData data = SaveSystem.LoadStatePlayer();
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
