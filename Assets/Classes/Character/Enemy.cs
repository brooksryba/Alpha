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
        SaveSystem.SaveState<EnemyData>(new EnemyData(this), gameObject.name);
    }

    public void LoadState()
    {
        EnemyData data = SaveSystem.LoadState<EnemyData>(gameObject.name) as EnemyData;
        if( data != null ) {
            gameObject.SetActive(data.active);
        }
    }
}
