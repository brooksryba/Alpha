
public class BasicAttack : Attack
{
    public string name = "Basic Attack";
    override public bool doAttack(string attackerName, string defenderName) {
        Character attacker = getCharacter(attackerName);
        Character defender = getCharacter(defenderName);

        defender.TakeDamage(5);
        return true;
    }
}
