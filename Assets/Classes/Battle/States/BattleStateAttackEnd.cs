using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStateAttackEnd : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;

        battleObjManager.battleSystemHud.RefreshAllHUDs();
    
        battleObjManager.chosenAttack = null;
        newState = new BattleStateGetAttacker();

        yield return new WaitForSeconds(2f);

        bool allDeadEnemies = battleSystemUtils.PartyDead(battleObjManager.enemyParty);
        bool allDeadPlayers = battleSystemUtils.PartyDead(battleObjManager.playerParty);
        if(allDeadEnemies || allDeadPlayers){
            newState = new BattleStateEnd();
        }      


    }
}
