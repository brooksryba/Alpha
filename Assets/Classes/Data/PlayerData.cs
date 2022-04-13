using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData {

    public List<ItemData> items;

    public PlayerData(Player player) {

        items = player.items;
    }

}


