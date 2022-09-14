using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : InventoryItemData
{
    public Gem(){
        title = "Gem";
        hp = 0.25;   
        mana = 0;
        message = "Health was increased by 25%.";
    }

    override public string Execute(Character user){
        user.multiplyHP(hp);
        return base.Execute(user);
    }
}