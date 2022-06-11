using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Archer : BaseCharacterClass
{
    public Archer(){
        className = "Archer";
        baseStats.Add("maxHp", 10);
        statProgression.Add("maxHp", 5);

        baseStats.Add("maxMana", 5);
        statProgression.Add("maxMana", 2);

        baseStats.Add("physicalAttack", 10);
        statProgression.Add("physicalAttack", 3);

        baseStats.Add("physicalDefense", 5);
        statProgression.Add("physicalDefense", 2);

        baseStats.Add("magicAttack", 3);
        statProgression.Add("magicAttack", 2);

        baseStats.Add("magicDefense", 5);
        statProgression.Add("magicDefense", 2);

        baseStats.Add("speed", 15);
        statProgression.Add("speed", 5);
        

    }

}
