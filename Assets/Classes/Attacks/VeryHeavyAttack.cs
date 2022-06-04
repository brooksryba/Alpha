

public class VeryHeavyAttack : Attack
{
    public string name = "Very Heavy Attack";

    private int _GetHealthCost(){
        return 5;
    }

    private int _GetManaCost(){
        return 10;
    }

    override public bool CheckAttackInputs()
    {
        return defenderName != "" & defenderName != null;
    }

    override public bool CheckAttackFeasible()
    {
        return GetCharacter(attackerName).currentHP > _GetHealthCost() & GetCharacter(attackerName).currentMana >= _GetManaCost();
    }

    public override int GetTotalDamageAi()
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

        if(attacker.useMana(_GetManaCost())){
            attacker.TakeDamage(_GetHealthCost());
            defender.TakeDamage(defender.currentHP);
        } else {
            return;
        }        
    
    }
}