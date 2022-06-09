using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleItem : BattleMoveBase
{
    public Dictionary<string, InventoryItemData> itemData = WorldItems.lookup;
    public BattleItem(){
        moveType = "Item";
    }

    public InventoryItemData GetWorldItemData(){
        return itemData[moveName];
    }

    
}
