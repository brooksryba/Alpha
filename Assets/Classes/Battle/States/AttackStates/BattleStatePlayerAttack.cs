using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatePlayerAttack : BattleState
{

    public override IEnumerator execute()
    {
        newState = this;

        _manager.battleSystemMenu.closeOptionSubmenu();
        _manager.battleSystemHud.canSelect = true;
        if(battleSystemUtils.ConfirmBattleMoveInputs(_manager.chosenBattleMove, _manager.charManager.attacker.GetComponent<Character>(), _manager.battleSystemHud.selection)){
            _manager.battleSystemHud.canSelect = false;        
           
            bool isAccepted = battleSystemUtils.ConfirmBattleMoveFeasibility(_manager.chosenBattleMove, _manager.charManager.attacker.GetComponent<Character>(), _manager.battleSystemHud.selection); 

            _manager.battleSystemHud.RefreshAllHUDs();
            if(isAccepted){
                if(_manager.battleSystemHud.selection)
                    _manager.SetDefender(_manager.battleSystemHud.selection.title);
                else
                    _manager.SetDefender("");
                newState = new BattleStateAttackMinigame();
                _manager.battleSystemHud.selection = null;

            }
            else {
                _manager.dialogueText.text = _manager.charManager.attackerName + " cannot choose this attack";
                yield return new WaitForSeconds(2f);
                newState = new BattleStatePlayerStart();
                _manager.battleSystemHud.selection = null;
            }
        }

        yield return new WaitForSeconds(0f);
    }
}
