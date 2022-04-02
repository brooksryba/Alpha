using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int level;
    public int health;

    void Start()
    {
        LoadState();
    }

    public void SaveState()
    {
        SaveSystem.SaveStateEnemy(this, gameObject.name);
    }

    public void LoadState()
    {
        EnemyData data = SaveSystem.LoadStateEnemy(gameObject.name);
        if( data != null ) {
            gameObject.SetActive(data.active);
        }
    }
}
