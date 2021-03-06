using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateEnemyAttack : BattleState
{

    override public IEnumerator execute()
    {
        newState = this;

        EnemyAttackChooser attackChooser = new EnemyAttackChooser();
        List<string> chosenAttackList = attackChooser.GetAttack(_manager.charManager.attackerName);
        _manager.chosenBattleMove = chosenAttackList[0];
        if(chosenAttackList[1] != "")
            _manager.dialogueText.text = _manager.charManager.attackerName + " attacks " + chosenAttackList[1] + " with " + _manager.chosenBattleMove + "!";
        else 
            _manager.dialogueText.text = _manager.charManager.attackerName + " uses " + _manager.chosenBattleMove + "!";
        yield return new WaitForSeconds(2f);

        _manager.SetDefender(chosenAttackList[1]);

        if(chosenAttackList[1] != "") _manager.charManager.defender = GameObject.Find(chosenAttackList[1]);
        
        newState = new BattleStateAttackMinigame();
            
        yield return new WaitForSeconds(0f);
    }
}
