using UnityEngine;
public class BasicAttack : Attack
{
    public string name = "Basic Attack";

    private int _GetAttackDamage(){
        // Character attacker = GetCharacter(attackerName);
        // return 5 * attacker.level;
        return 5;
    }

    override public bool CheckAttackInputs()
    {
        return defenderName != "" & defenderName != null;
    }

    override public bool CheckAttackFeasible()
    {
        return true;
    }

    public override int GetTotalDamageAi()
    {
        if(defenderName != ""){
            return Mathf.Min(GetCharacter(defenderName).currentHP, _GetAttackDamage());
        } else {
            return -1;
        }
    }

    override public void _DoAttack() {
        Character attacker = GetCharacter(attackerName);
        Character defender = GetCharacter(defenderName);

        defender.TakeDamage((int)(_GetAttackDamage()*damageMultiplier));
    }
}
