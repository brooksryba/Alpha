using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackData
{
    public int mana;
    public int damage;

    public AttackData(int damage, int mana)
    {
        this.damage = damage;
        this.mana = mana;
    }
}

public static class Attacks
{
    public static Dictionary<string, AttackData> lookup = new Dictionary<string, AttackData>() {
        {"Basic Attack", new AttackData(25, 0)},
        {"Heavy Attack", new AttackData(50, 10)}
    };
}