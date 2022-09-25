using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatePlayerAttack : BattleState
{

    public override IEnumerator enter()
    {
        Toast("Choose a target for "+_manager.charManager.attackerName+":");
        _manager.battleSystemMenu.closeOptionSubmenu();
        _manager.battleSystemHud.canSelect = true;
        return base.enter();
    }
    public override IEnumerator execute()
    {
        if(battleSystemUtils.ConfirmBattleMoveInputs(_manager.chosenBattleMove, _manager.charManager.attacker.GetComponent<Character>(), _manager.battleSystemHud.selection)){
            _manager.battleSystemHud.canSelect = false;        
           
            bool isAccepted = battleSystemUtils.ConfirmBattleMoveFeasibility(_manager.chosenBattleMove, _manager.charManager.attacker.GetComponent<Character>(), _manager.battleSystemHud.selection); 

            if(isAccepted){
                if(_manager.battleSystemHud.selection)
                    _manager.SetDefender(_manager.battleSystemHud.selection.title);
                else
                    _manager.SetDefender("");
                Transition(new BattleStateAttackMinigame());
            }
            else {
                Toast(_manager.charManager.attackerName + " cannot choose this attack");
                Transition(new BattleStatePlayerStart());
            }

            _manager.battleSystemHud.selection = null;
        }

        return base.execute();
    }
}
