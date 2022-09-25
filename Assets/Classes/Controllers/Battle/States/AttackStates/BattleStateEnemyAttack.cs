using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateEnemyAttack : BattleState
{
    public override IEnumerator enter()
    {
        EnemyAttackChooser attackChooser = new EnemyAttackChooser();
        List<string> chosenAttackList = attackChooser.GetAttack(_manager.charManager.attackerName);
        _manager.chosenBattleMove = chosenAttackList[0];

        if(chosenAttackList[1] != "")
            Toast(_manager.charManager.attackerName + " attacks " + chosenAttackList[1] + " with " + _manager.chosenBattleMove + "!");
        else 
            Toast(_manager.charManager.attackerName + " uses " + _manager.chosenBattleMove + "!");

        _manager.SetDefender(chosenAttackList[1]);
        if(chosenAttackList[1] != "") _manager.charManager.defender = GameObject.Find(chosenAttackList[1]);
        return base.enter(2f);
    }
    override public IEnumerator execute()
    {
        Transition(new BattleStateAttackMinigame());
        return base.execute();
    }
}
