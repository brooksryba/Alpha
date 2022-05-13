using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateEnemyAttack : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;

        battleObjManager.dialogueText.text = battleObjManager.enemyUnit.title + " attacks " + battleObjManager.playerUnit.title + "!";

        yield return new WaitForSeconds(1f);

        // @todo - this is where the enemies AI should be implemented

        bool isDead = battleObjManager.playerUnit.TakeDamage(5); 

        battleObjManager.battleSystemHud.RefreshAllHUDs();

        yield return new WaitForSeconds(1f);

        newState = new BattleStateGetAttacker();

        if(isDead){
            bool allDead = battleSystemUtils.PartyDead(battleObjManager.playerParty);
            if(allDead){
                newState = new BattleStateEnd();
            }
        }

        yield return new WaitForSeconds(0f);
    }
}
