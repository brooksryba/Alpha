using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revive : InventoryItemData
{
    public Revive(){
        title = "Revive";
        hp = 1;   
        mana = 0;
        message = "Health was increased by 100%.";
    }

    override public string Execute(Character user){
        user.multiplyHP(hp);
        return base.Execute(user);
    }
}