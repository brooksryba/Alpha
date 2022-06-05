using UnityEngine;

public class Attack
{

    public string attackerName;
    public string defenderName;
    public double damageMultiplier;
    public string minigameName;
    public string defenseMinigameName;
    public string animationName = "";
    


    public Character GetCharacter(string id)
    {
        // currently cannot use battle system utils function because utils uses this script causing stack overflow
        return GameObject.Find(id).GetComponent<Character>();
    }

    public bool CanUseAttack(){
        return CheckAttackInputs() & CheckAttackFeasible();
    }
    public void DoAttack() {
        if(CanUseAttack())
            this._DoAttack();

    }

    virtual public void _DoAttack() {
        // executes the attack and returns true if the attack can be executed. Else returns false.
        return;
    }

    virtual public bool CheckAttackInputs(){
        // this function returns true if all inputs for the attack are met (e.g. it has a target if a target is needed)
        return false;
    }

    virtual public bool CheckAttackFeasible(){
        // this function returns true if the player has enough mana, health, etc to execute attack
        return false;
    }

    virtual public int GetTotalDamageAi(){
        // for enemy ai, maybe other uses down the road, calculates the total potential damage dealt
        return -1;
    }


    public Attack GetAttackClass(string attackName){
        switch(attackName){
            case "Basic Attack":
                return new BasicAttack();
            case "Heavy Attack":
                return new HeavyAttack();
            case "Very Heavy Attack":
                return new VeryHeavyAttack();
            case "Spread Attack":
                return new SpreadAttack();
            default:
                return this;
        }
    }

}