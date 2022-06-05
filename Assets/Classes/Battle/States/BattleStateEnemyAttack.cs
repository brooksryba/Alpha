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
        battleObjManager.chosenAttack = chosenAttackList[0];
        battleObjManager.dialogueText.text = battleObjManager.enemyUnit.title + " attacks " + chosenAttackList[1] + " with " + battleObjManager.chosenAttack + "!";
        yield return new WaitForSeconds(1f);

        battleObjManager.attacker = GameObject.Find(battleObjManager.enemyUnit.title);
        battleObjManager.defender = GameObject.Find(chosenAttackList[1]);

        battleSystemUtils.DoAttack(battleObjManager.chosenAttack, battleSystemUtils.GetCharacter(battleObjManager.enemyUnit.title), battleSystemUtils.GetCharacter(chosenAttackList[1]));
        
        newState = new BattleStateAttackAnimationApproach();
            
        yield return new WaitForSeconds(0f);
    }
}
