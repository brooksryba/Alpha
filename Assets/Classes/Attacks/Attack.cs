using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack
{

    virtual public bool doAttack(string attackerName, string defenderName) {
        return false;
    }

    public Character getCharacter(string characterName){
        return GameObject.Find(characterName).GetComponent<Character>();
    }

    public Attack getAttackClass(string attackName){
        switch(attackName){
            case "Basic Attack":
                return new BasicAttack();
            case "Heavy Attack":
                return new HeavyAttack();
            case "Very Heavy Attack":
                return new VeryHeavyAttack();
            default:
                return this;
        }
    }


}


public class BasicAttack : Attack
{
    public string name = "Basic Attack";
    override public bool doAttack(string attackerName, string defenderName) {
        Character attacker = getCharacter(attackerName);
        Character defender = getCharacter(defenderName);

        defender.TakeDamage(5);
        return true;    
    }
}


public class HeavyAttack : Attack
{
    public string name = "Heavy Attack";
    override public bool doAttack(string attackerName, string defenderName) {
        Character attacker = getCharacter(attackerName);
        Character defender = getCharacter(defenderName);

        if(attacker.useMana(5)){
            defender.TakeDamage(15);
            return true;
        } else {
            return false;
        }        
    
    }
}



public class VeryHeavyAttack : Attack
{
    public string name = "Very Heavy Attack";
    override public bool doAttack(string attackerName, string defenderName) {
        Character attacker = getCharacter(attackerName);
        Character defender = getCharacter(defenderName);

        if(attacker.currentHP <= 5){
            return false;
        }

        if(attacker.useMana(10)){
            attacker.TakeDamage(5);
            defender.TakeDamage(defender.currentHP);
            return true;
        } else {
            return false;
        }        
    
    }
}