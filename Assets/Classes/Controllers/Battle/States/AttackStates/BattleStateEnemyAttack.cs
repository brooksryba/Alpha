using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateEnemyAttack : BattleState
{

    override public IEnumerator execute()
    {
        newState = this;

        EnemyAttackChooser attackChooser = new EnemyAttackChooser();
        (string battleMove, string targetName) = attackChooser.GetAttack(_manager.charManager.attackerName);

        _manager.chosenBattleMove = battleMove;

        if(targetName != "")
            newMessage = _manager.charManager.attackerName + " attacks " + targetName + " with " + _manager.chosenBattleMove + "!";
        else 
            newMessage = _manager.charManager.attackerName + " uses " + _manager.chosenBattleMove + "!";
        yield return new WaitForSeconds(2f);

        _manager.SetDefender(targetName);

        if(targetName != "") _manager.charManager.defender = GameObject.Find(targetName);
        
        newState = new BattleStateAttackMinigame();
            
        yield return new WaitForSeconds(0f);
    }
}
