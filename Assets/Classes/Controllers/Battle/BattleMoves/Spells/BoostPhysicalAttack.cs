using UnityEngine;
public class BoostPhysicalAttack : Spell
{
   public BoostPhysicalAttack(){
        moveName = "Boost Physical Attack";
        needsTarget = true;
        minigameName = "";
        defenseMinigameName = "";
    }

    public double GetBoostAddition(){
        return 5.0;
    }

    public double GetBoostMultiplier(){
        return 2.0;
    }

    public int GetManaCost(){
        return 10;
    }

    public int GetTurnDuration(){
        return 3;
    }

    override public bool CheckFeasibility()
    {
        return GetCharacter(userName).currentMana >= GetManaCost();
    }

    override public int GetMoveValueForAi()
    {
        if(IsUserAndTargetSameTeam())
            return 5;
        return -5;
    }

    override public void _ExecuteBattleMove() {
        Character caster = GetCharacter(userName);
        Character target = GetCharacter(targetName);
        caster.UseMana(GetManaCost());
        _manager.battleBonusManager.AddBonus(targetName, "physicalAttack", GetBoostMultiplier(), GetBoostAddition(), GetTurnDuration());
        
    }
}
