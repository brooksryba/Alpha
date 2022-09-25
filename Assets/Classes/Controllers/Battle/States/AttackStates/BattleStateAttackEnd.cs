using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStateAttackEnd : BattleState
{
    public override IEnumerator enter()
    {
        _manager.battleBonusManager.IncrementPlayerTurn(_manager.charManager.attacker.GetComponent<Character>().title);
        _manager.battleSystemHud.RefreshAllHUDs();
        _manager.chosenBattleMove = "";
        return base.enter();
    }
    override public IEnumerator execute()
    {
        Transition(new BattleStateGetAttacker());
        if(battleSystemUtils.PartyDead(_manager.charManager.enemyParty) || battleSystemUtils.PartyDead(_manager.charManager.playerParty)){
            Transition(new BattleStateEnd());
        }      

        return base.execute();
    }
}
