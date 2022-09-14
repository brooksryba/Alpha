using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonSpell : Spell
{
 public PoisonSpell(){
        moveName = "Poison Spell";
        needsTarget = true;
        minigameName = "";
        defenseMinigameName = "";
    }

    public int GetTurnDuration(){
        return 3;
    }

    public double GetDamage(){
        return 5.0;
    }

    public int GetManaCost(){
        return 10;
    }

    override public bool CheckFeasibility()
    {
        return GetCharacter(userName).currentMana >= GetManaCost();
    }

    override public int GetMoveValueForAi()
    {
        if(IsUserAndTargetSameTeam())
            return -5;
        return (int)(GetDamage()*GetTurnDuration());
    }

    override public void _ExecuteBattleMove() {
        Character caster = GetCharacter(userName);
        Character target = GetCharacter(targetName);
        caster.useMana(GetManaCost());
        _manager.battleBonusManager.AddBonus(targetName, "currentHp", 0.0, -1*GetDamage(), GetTurnDuration());
        
    }
}
