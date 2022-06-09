using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemData {
    public string title;
    public string itemName;
    public bool active;

    public ItemData(InventoryItem item) {
        title = item.gameObject.name;
        active = item.gameObject.activeSelf;
    }
}
