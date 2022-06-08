using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BasicAttack : Attack
{
    public BasicAttack(){
        moveName = "Basic Attack";
        needsTarget = true;
        minigameName = "BattleMinigameBasic";
        defenseMinigameName = "BattleMinigameBasic";
    }

    public int GetAttackDamage(){
        int baseDamage = 5;
        Debug.Log("Calling basic attack. Users phys attack " + GetCharacter(userName).physicalAttack.ToString() + 
        "| Attack dealt: " + (battleObjManager.battleBonusManager.GetBattleStat(userName, "physicalAttack", GetCharacter(userName).physicalAttack + baseDamage)).ToString());
        return battleObjManager.battleBonusManager.GetBattleStat(userName, "physicalAttack", GetCharacter(userName).physicalAttack + baseDamage);
    }


    override public bool CheckFeasibility()
    {
        return true;
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

        defender.TakeDamage((int)(GetAttackDamage()*minigameMultiplier));
    }
}
