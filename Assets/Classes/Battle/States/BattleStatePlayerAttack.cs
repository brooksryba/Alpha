using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatePlayerAttack : BattleState
{

    public override IEnumerator execute()
    {
        newState = this;

        battleObjManager.battleSystemMenu.closeOptionSubmenu();
        battleObjManager.battleSystemHud.canSelect = true;
        if(battleSystemUtils.ConfirmAttackInputs(battleObjManager.chosenAttack, battleObjManager.playerUnit, battleObjManager.battleSystemHud.selection)){
            battleObjManager.battleSystemHud.canSelect = false;        
            battleObjManager.enemyUnit = battleObjManager.battleSystemHud.selection;
           
            bool isAccepted = battleSystemUtils.ConfirmCanUseAttack(battleObjManager.chosenAttack, battleObjManager.playerUnit, battleObjManager.enemyUnit); 
            battleObjManager.battleSystemHud.selection = null;

            battleObjManager.battleSystemHud.RefreshAllHUDs();
            if(isAccepted){

                newState = new BattleStateAttackMinigame();

            }
            else {
                battleObjManager.dialogueText.text = battleObjManager.playerUnit.title + " cannot choose this attack";
                yield return new WaitForSeconds(2f);
                newState = new BattleStatePlayerStart();
            }
        }

        yield return new WaitForSeconds(0f);
    }
}
