using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Warrior : BaseCharacterClass
{
    public Warrior()
    {
        className = "Warrior";
        baseStats.Add("maxHp", 15);
        statProgression.Add("maxHp", 10);

        baseStats.Add("maxMana", 5);
        statProgression.Add("maxMana", 3);

        baseStats.Add("physicalAttack", 10);
        statProgression.Add("physicalAttack", 3);

        baseStats.Add("physicalDefense", 10);
        statProgression.Add("physicalDefense", 3);

        baseStats.Add("magicAttack", 3);
        statProgression.Add("magicAttack", 1);

        baseStats.Add("magicDefense", 5);
        statProgression.Add("magicDefense", 1.5);

        baseStats.Add("speed", 5);
        statProgression.Add("speed", 2);
        

    }

}
