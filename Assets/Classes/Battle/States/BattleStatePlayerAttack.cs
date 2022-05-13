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
        if(battleObjManager.battleSystemHud.selection != null){
            battleObjManager.battleSystemHud.canSelect = false;        
            battleObjManager.enemyUnit = battleObjManager.battleSystemHud.selection;
            battleObjManager.battleSystemHud.selection = null;

            bool isAccepted = battleSystemUtils.DoAttack(battleObjManager.attackReference, ref battleObjManager.playerUnit, ref battleObjManager.enemyUnit);
            bool isDead = battleObjManager.enemyUnit.currentHP <= 0;

            battleObjManager.attackReference = null;
            battleObjManager.battleSystemHud.RefreshAllHUDs();
            if(isAccepted){
                
                battleObjManager.dialogueText.text = "The attack is successful";
                battleObjManager.battleSystemHud.RefreshAllHUDs();
                newState = new BattleStateGetAttacker();
                yield return new WaitForSeconds(2f);
                if(isDead){
                    bool allDead = battleSystemUtils.PartyDead(battleObjManager.enemyParty);
                    if(allDead){
                        newState = new BattleStateEnd();
                    }
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
