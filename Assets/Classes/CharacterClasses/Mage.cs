using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Mage : BaseCharacterClass
{
    public Mage(){
        className = "Mage";
        baseStats.Add("maxHp", 10);
        statProgression.Add("maxHp", 5);

        baseStats.Add("maxMana", 20);
        statProgression.Add("maxMana", 10);

        baseStats.Add("physicalAttack", 5);
        statProgression.Add("physicalAttack", 1.5);

        baseStats.Add("physicalDefense", 5);
        statProgression.Add("physicalDefense", 1.5);

        baseStats.Add("magicAttack", 10);
        statProgression.Add("physicalAttack", 3);

        baseStats.Add("magicDefense", 10);
        statProgression.Add("magicDefense", 2);

        baseStats.Add("speed", 10);
        statProgression.Add("speed", 3);
        

    }

}
