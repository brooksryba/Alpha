using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Jack : BaseCharacterClass
{
    public Jack(){
        className = "Jack";
        baseStats.Add("maxHp", 15);
        statProgression.Add("maxHp", 10);

        baseStats.Add("maxMana", 5);
        statProgression.Add("maxMana", 3);

        baseStats.Add("physicalAttack", 7);
        statProgression.Add("physicalAttack", 2);

        baseStats.Add("physicalDefense", 7);
        statProgression.Add("physicalDefense", 2);

        baseStats.Add("magicAttack", 7);
        statProgression.Add("magicAttack", 2);

        baseStats.Add("magicDefense", 7);
        statProgression.Add("magicDefense", 2);

        baseStats.Add("speed", 7);
        statProgression.Add("speed", 10);
        

    }

}
