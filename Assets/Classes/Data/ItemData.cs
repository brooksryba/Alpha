using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData {
    public bool active;

    public ItemData(InventoryItem item) {
        active = item.gameObject.activeSelf;
    }
}
