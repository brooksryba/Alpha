

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