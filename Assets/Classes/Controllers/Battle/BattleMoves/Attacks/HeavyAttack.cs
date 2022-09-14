using UnityEngine;

public class HeavyAttack : Attack
{
    public HeavyAttack(){
        moveName = "Heavy Attack";
        needsTarget = true;
        minigameName = "BattleMinigameBasic";
        defenseMinigameName = "BattleMinigameBasic";
    }

    public int GetAttackDamage(){
        return 15;
    }

    public int GetManaCost(){
        return 5;
    }


    override public bool CheckFeasibility()
    {
        return GetCharacter(userName).currentMana >= GetManaCost();
    }

    override public int GetMoveValueForAi()
    {
        if(targetName != "" && !IsUserAndTargetSameTeam()){
            return Mathf.Min(GetCharacter(targetName).currentHP, GetAttackDamage());
        } else {
            return -1;
        }
    }

    
    override public void _ExecuteBattleMove() {
        Character attacker = GetCharacter(userName);
        Character defender = GetCharacter(targetName);

        if(attacker.useMana(GetManaCost())){
            defender.TakeDamage((int)(GetAttackDamage()*minigameMultiplier));
        } else {
            return;
        }        
    
    }
}