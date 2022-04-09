using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
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
        this.currentHP = this.level * 10;
        this.currentMana = this.level * 5;
        this.maxHP = this.level * 10;
        this.maxMana = this.level * 5;
        EnemyData data = SaveSystem.LoadState<EnemyData>(gameObject.name) as EnemyData;
        if (data != null)
        {
            gameObject.SetActive(data.active);
        }
    }
}
