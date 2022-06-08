using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateEnemyAttack : BattleState
{

    override public IEnumerator execute()
    {
        newState = this;

        EnemyAttackChooser attackChooser = new EnemyAttackChooser();
        List<string> chosenAttackList = attackChooser.GetAttack(battleObjManager.enemyUnit.title);
        battleObjManager.chosenBattleMove = chosenAttackList[0];
        battleObjManager.dialogueText.text = battleObjManager.enemyUnit.title + " attacks " + chosenAttackList[1] + " with " + battleObjManager.chosenBattleMove + "!";
        yield return new WaitForSeconds(2f);

        battleObjManager.attacker = GameObject.Find(battleObjManager.enemyUnit.title);
        battleObjManager.defender = GameObject.Find(chosenAttackList[1]);

        battleObjManager.attacker = GameObject.Find(battleObjManager.enemyUnit.title);
        battleObjManager.defender = null;
        if(chosenAttackList[1] != "") battleObjManager.defender = GameObject.Find(chosenAttackList[1]);
        
        newState = new BattleStateAttackMinigame();
            
        yield return new WaitForSeconds(0f);
    }
}
