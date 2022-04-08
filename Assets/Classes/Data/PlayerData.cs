using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public int level;
    public int currentHP;
    public int currentMana;
    public float[] position;
    public List<ItemData> items;

    public PlayerData(Player player) {
        level = player.level;
        currentHP = player.currentHP;
        currentMana = player.currentMana;

        position = new float[2];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;

        items = player.items;
    }

}
