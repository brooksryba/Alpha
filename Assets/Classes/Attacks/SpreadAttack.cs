using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SpreadAttack : Attack
{
    public string name = "Spread Attack";
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

    override public bool CheckAttackInputs()
    {
        return true;
    }

    override public bool CheckAttackFeasible()
    {
        return true;
    }

    public override int GetAttackDamage()
    {

        int damage = 0;
        _setDefenders();
        foreach(var d in defenderList){
            damage += Mathf.Min(GetCharacter(d).currentHP, 5);
        }

        return damage;
        
    }
    override public void _DoAttack() {
        _setDefenders();
        foreach(var d in defenderList){
            GetCharacter(d).TakeDamage(5);
        }
    }
}
