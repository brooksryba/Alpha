

public class VeryHeavyAttack : Attack
{
    public string name = "Very Heavy Attack";
    override public bool CheckAttackInputs()
    {
        return defenderName != "" & defenderName != null;
    }

    override public bool CheckAttackFeasible()
    {
        return GetCharacter(attackerName).currentHP > 5 & GetCharacter(attackerName).currentMana >= 10;
    }

    public override int GetAttackDamage()
    {
        if(defenderName != ""){
            return GetCharacter(defenderName).currentHP;
        } else {
            return -1;
        }
    }

    override public void _DoAttack() {
        Character attacker = GetCharacter(attackerName);
        Character defender = GetCharacter(defenderName);

        if(attacker.useMana(10)){
            attacker.TakeDamage(5);
            defender.TakeDamage(defender.currentHP);
        } else {
            return;
        }        
    
    }
}