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