using UnityEngine;
public class BasicAttack : Attack
{
    public string name = "Basic Attack";

    override public bool CheckAttackInputs()
    {
        return defenderName != "" & defenderName != null;
    }

    override public bool CheckAttackFeasible()
    {
        return true;
    }

    public override int GetAttackDamage()
    {
        if(defenderName != ""){
            return Mathf.Min(GetCharacter(defenderName).currentHP, 5);
        } else {
            return -1;
        }
    }

    override public void _DoAttack() {
        Character attacker = GetCharacter(attackerName);
        Character defender = GetCharacter(defenderName);

        defender.TakeDamage((int)(5*damageMultiplier));
    }
}
