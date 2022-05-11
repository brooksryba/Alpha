public class BattleSystemUtils
{
    public Character GetCharacter(string id)
    {
        return GameObject.Find(id).GetComponent<Character>();
    }

    public bool DoAttack(AttackData attack, ref Character attacker, ref Character defender)
    {
        if(attacker.useMana(attack.mana)){
            defender.TakeDamage(attack.damage);
            return true;
        } else {
            return false;
        }        
    }

    public bool PartyDead(List<string> partyMembers)
    {
        foreach(var id in partyMembers)
        {
            Character member = GetCharacter(id);
            if(member.currentHP > 0)
                return false;
        }
        return true;
    }
}