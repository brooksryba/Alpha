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
        {"Revive", new Revive()},
        {"Gem", new Gem()}
    };
}

public static class BattleItems
{
    public static Dictionary<string, InventoryItemData> lookup = new Dictionary<string, InventoryItemData>() {
        {"Revive", new Revive()},
        {"Gem", new Gem()}
    };
}

