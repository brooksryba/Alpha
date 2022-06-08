using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStateAttackEnd : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;

        battleObjManager.battleSystemHud.RefreshAllHUDs();
        battleObjManager.battleBonusManager.IncrementPlayerTurn(battleObjManager.attacker.GetComponent<Character>().title);
    
        battleObjManager.chosenBattleMove = null;
        newState = new BattleStateGetAttacker();

        yield return new WaitForSeconds(2f);

        if(battleSystemUtils.PartyDead(battleObjManager.enemyParty) || battleSystemUtils.PartyDead(battleObjManager.playerParty)){
            newState = new BattleStateEnd();
        }      


    }
}
