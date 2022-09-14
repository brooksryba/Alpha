

public class VeryHeavyAttack : Attack
{
    public VeryHeavyAttack(){
        moveName = "Very Heavy Attack";
        needsTarget = true;
        minigameName = "";
        defenseMinigameName = "";
    }

    public int GetHealthCost(){
        return 5;
    }

    public int GetManaCost(){
        return 10;
    }

    override public bool CheckFeasibility()
    {
        return GetCharacter(userName).currentHP > GetHealthCost() & GetCharacter(userName).currentMana >= GetManaCost();
    }

    public override int GetMoveValueForAi()
    {
        if(targetName != "" && !IsUserAndTargetSameTeam()){
            return GetCharacter(targetName).currentHP;
        } else {
            return -1;
        }
    }

    override public void _ExecuteBattleMove() {
        Character attacker = GetCharacter(userName);
        Character defender = GetCharacter(targetName);

        if(attacker.useMana(GetManaCost())){
            attacker.TakeDamage(GetHealthCost());
            defender.TakeDamage(defender.currentHP);
        } else {
            return;
        }        
    
    }
}