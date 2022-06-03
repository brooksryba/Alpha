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


                battleSystemUtils.DoAttack(battleObjManager.chosenAttack, battleObjManager.playerUnit, battleObjManager.enemyUnit);
                battleObjManager.dialogueText.text = "The attack is successful";
                battleObjManager.battleSystemHud.RefreshAllHUDs();
                

                battleObjManager.chosenAttack = null;
                newState = new BattleStateGetAttacker();

                yield return new WaitForSeconds(2f);

                bool allDead = battleSystemUtils.PartyDead(battleObjManager.enemyParty);
                if(allDead){
                    newState = new BattleStateEnd();
                }
                
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
