using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpreadAttack : Attack
{
    public string name = "Spread Attack";
    public SpreadAttack(){
        minigameName = "BattleMinigameBasic";
    }

    public BattleObjectManager battleObjManager = GameObject.Find("BattleObjectManager").GetComponent<BattleObjectManager>();

    public List<string> defenderList;

    private void _setDefenders(){
        List<string> startingList = new List<string>();
        defenderList = new List<string>();
        if(battleObjManager.playerParty.Contains(attackerName)){
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

    private int _GetAttackDamage(){
        return 5;
    }

    override public bool CheckAttackInputs()
    {
        return true;
    }

    override public bool CheckAttackFeasible()
    {
        return true;
    }

    public override int GetTotalDamageAi()
    {

        int damage = 0;
        _setDefenders();
        foreach(var d in defenderList){
            damage += Mathf.Min(GetCharacter(d).currentHP, _GetAttackDamage());
        }

        return damage;
        
    }
    override public void _DoAttack() {
        _setDefenders();
        foreach(var d in defenderList){
            GetCharacter(d).TakeDamage((int)(_GetAttackDamage()*damageMultiplier));
        }
    }
}
