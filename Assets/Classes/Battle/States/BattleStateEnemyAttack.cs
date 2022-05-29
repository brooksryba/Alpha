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
        battleObjManager.dialogueText.text = battleObjManager.enemyUnit.title + " attacks " + chosenAttackList[1] + " with " + chosenAttackList[0] + "!";
        yield return new WaitForSeconds(1f);

        bool isDead = battleSystemUtils.DoAttack(chosenAttackList[0], battleSystemUtils.GetCharacter(battleObjManager.enemyUnit.title), battleSystemUtils.GetCharacter(chosenAttackList[1]));

        battleObjManager.battleSystemHud.RefreshAllHUDs();

        yield return new WaitForSeconds(1f);

        newState = new BattleStateGetAttacker();


        bool allDead = battleSystemUtils.PartyDead(battleObjManager.playerParty);
        if(allDead){
            newState = new BattleStateEnd();
        }
        

        yield return new WaitForSeconds(0f);
    }
}
