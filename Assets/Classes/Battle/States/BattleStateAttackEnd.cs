using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStateAttackEnd : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;

        _manager.battleBonusManager.IncrementPlayerTurn(_manager.attacker.GetComponent<Character>().title);

        _manager.battleSystemHud.RefreshAllHUDs();
    
        _manager.chosenBattleMove = null;
        newState = new BattleStateGetAttacker();

        yield return new WaitForSeconds(2f);

        if(battleSystemUtils.PartyDead(_manager.enemyParty) || battleSystemUtils.PartyDead(_manager.playerParty)){
            newState = new BattleStateEnd();
        }      


    }
}
