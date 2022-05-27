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