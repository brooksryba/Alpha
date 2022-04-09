using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public int level;
    public int currentHP;
    public int currentMana;

    public List<ItemData> items;

    public PlayerData(Player player) {
        level = player.level;
        currentHP = player.currentHP;
        currentMana = player.currentMana;

        items = player.items;
    }

}
