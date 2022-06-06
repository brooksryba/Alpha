using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpreadAttack : Attack
{

    public SpreadAttack(){
        moveName = "Spread Attack";
        needsTarget = false;
        minigameName = "BattleMinigameBasic";
        defenseMinigameName = "BattleMinigameBasic";
    }

    public List<string> defenderList;

    public void SetDefenders(){
        List<string> startingList = new List<string>();
        defenderList = new List<string>();
        if(!IsEnemyUser()){
            startingList = battleObjManager.enemyParty;
        } else {
            startingList = battleObjManager.playerParty;
        }

        foreach(var p in startingList){
            if(GetCharacter(p).currentHP > 0){
                defenderList.Add(p);
            }
        }
    }

    public int GetAttackDamage(){
        return 5;
    }

    override public bool CheckFeasibility()
    {
        return true;
    }

    public override int GetMoveValueForAi()
    {

        int damage = 0;
        SetDefenders();
        foreach(var d in defenderList){
            damage += Mathf.Min(GetCharacter(d).currentHP, GetAttackDamage());
        }

        return damage;
        
    }
    override public void _ExecuteBattleMove() {
        SetDefenders();
        foreach(var d in defenderList){
            GetCharacter(d).TakeDamage((int)(GetAttackDamage()*minigameMultiplier));
        }
    }
}
