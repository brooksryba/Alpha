using UnityEngine;

public class HeavyAttack : Attack
{
    public string name = "Heavy Attack";

    private int _GetAttackDamage(){
        return 15;
    }

    private int _GetManaCost(){
        return 5;
    }

    override public bool CheckAttackInputs()
    {
        return defenderName != "" & defenderName != null;
    }

    override public bool CheckAttackFeasible()
    {
        return GetCharacter(attackerName).currentMana >= _GetManaCost();
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

        if(attacker.useMana(_GetManaCost())){
            defender.TakeDamage((int)(_GetAttackDamage()*damageMultiplier));
        } else {
            return;
        }        
    
    }
}