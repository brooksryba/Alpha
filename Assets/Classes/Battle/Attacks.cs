using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Attacks
{


   public static bool basicAttack(Character attacker, Character defender)
    {

        defender.TakeDamage(2*attacker.level + 5);
        return true;

    }


    public static bool heavyAttack(Character attacker, Character defender)
    {
        if(attacker.useMana(5)){
            defender.TakeDamage(2*attacker.level + 10);
            return true;
        } else {
            return false;
        }
        
    }


}

