using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateAttackMinigame : BattleState
{

    override public IEnumerator execute()
    {
        newState = this;
        battleObjManager.dialogueText.text = "Complete the Minigame for extra Attack!";
        if(!minigame.minigameInProgress){
            //yield return new WaitForSeconds(1f);
            Debug.Log("Minigame is setting up");
            minigame = battleSystemUtils.getAttackMinigame(battleObjManager.chosenAttack);
            //minigame.resetMinigame();
            minigame.setupMinigame();
            minigame.minigameInProgress = true;
            minigame.runMinigame();

        }

        if(minigame.minigameComplete){
            minigame.minigameInProgress = false;
            
            battleSystemUtils.DoAttack(battleObjManager.chosenAttack, battleObjManager.playerUnit, battleObjManager.enemyUnit, minigame.bonusMultiplier);
            
            if(minigame.completedSuccessfully){
                battleObjManager.dialogueText.text = "The attack is successful with extra damage!";
            } else {
                battleObjManager.dialogueText.text = "The attack is successful without extra damage :(";
            }
            minigame.breakdownMinigame();
            minigame.resetMinigame();
            
            battleObjManager.battleSystemHud.RefreshAllHUDs();
            

            battleObjManager.chosenAttack = null;
            newState = new BattleStateGetAttacker();

            yield return new WaitForSeconds(2f);

            bool allDead = battleSystemUtils.PartyDead(battleObjManager.enemyParty);
            if(allDead){
                newState = new BattleStateEnd();
            }

        }

        yield return new WaitForSeconds(0f);
    }
}
