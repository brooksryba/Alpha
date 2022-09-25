using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToxicGem : InventoryItemData
{
    public ToxicGem(){
        title = "Toxic Gem";
        hp = 0.50;   
        mana = -0.25;
        message = "Health was increased by 50%, but mana went down 25%.";
    }

    override public string Execute(Character user){
        user.multiplyHP(hp);
        user.MultiplyMana(mana);
        return base.Execute(user);
    }
}