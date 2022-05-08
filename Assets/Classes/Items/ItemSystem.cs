using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemData
{
    public double hp;
    public double mana;
    public string message;

    public InventoryItemData(double hp, double mana, string message)
    {
        this.hp = hp;
        this.mana = mana;
        this.message = message;
    }
}

public static class WorldItems
{
    public static Dictionary<string, InventoryItemData> lookup = new Dictionary<string, InventoryItemData>() {
        {"Gem", new InventoryItemData(0.0, 0.25, "Mana was increased by 25%.")},
        {"Toxic Gem", new InventoryItemData(-0.1, 0.25, "Mana was increased by 25%, but HP decreased by 10%.")}
    };
}

public static class BattleItems
{
    public static Dictionary<string, InventoryItemData> lookup = new Dictionary<string, InventoryItemData>() {
        {"Gem", new InventoryItemData(0.0, 0.25, "Mana was increased by 25%.")},
        {"Toxic Gem", new InventoryItemData(-0.1, 0.25, "Mana was increased by 25%, but HP decreased by 10%.")}
    };
}