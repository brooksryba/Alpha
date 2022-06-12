using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemData
{
    public string title;
    public double hp;
    public double mana;
    public string message;

    virtual public string Execute(Character user){
        string text = user.title + " used a";
        if("aeiou".Contains(title[0].ToString())) {
            text += "n";
        }
        text += " " + title.ToLower() + ".";                                
        if( message != "" )
        {
            text += "\n" + message;
        }

        user.items.Remove(user.GetInventoryItemRefs()[title]);

        return text;
    }
}

public static class WorldItems
{
    public static Dictionary<string, InventoryItemData> lookup = new Dictionary<string, InventoryItemData>() {
        //{"Revive", new InventoryItemData(1.0, 0.0, "Health was increased by 100%.")},
        {"Gem", new Gem()},
        //{"Toxic Gem", new InventoryItemData(-0.1, 0.25, "Mana was increased by 25%, but HP decreased by 10%.")}
    };
}

public static class BattleItems
{
    public static Dictionary<string, InventoryItemData> lookup = new Dictionary<string, InventoryItemData>() {
        //{"Revive", new InventoryItemData(1.0, 0.0, "Health was increased by 100%.")},
        {"Gem", new Gem()},
        //{"Toxic Gem", new InventoryItemData(-0.1, 0.25, "Mana was increased by 25%, but HP decreased by 10%.")}
    };
}

