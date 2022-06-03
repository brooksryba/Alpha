using UnityEngine;

public class HeavyAttack : Attack
{
    public string name = "Heavy Attack";

    override public bool CheckAttackInputs()
    {
        return defenderName != "" & defenderName != null;
    }

    override public bool CheckAttackFeasible()
    {
        return GetCharacter(attackerName).currentMana >= 5;
    }

    public override int GetAttackDamage()
    {
        if(defenderName != ""){
            return Mathf.Min(GetCharacter(defenderName).currentHP, 15);
        } else {
            return -1;
        }
    }

    
    override public void _DoAttack() {
        Character attacker = GetCharacter(attackerName);
        Character defender = GetCharacter(defenderName);

        if(attacker.useMana(5)){
            defender.TakeDamage(15);
        } else {
            return;
        }        
    
    }
}