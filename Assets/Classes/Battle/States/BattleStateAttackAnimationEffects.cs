using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStateAttackAnimationEffects : BattleState
{

    override public IEnumerator execute()
    {
        newState = this;

        GameObject effect = battleObjManager.attacker.transform.GetChild(0).Find(battleObjManager.chosenAttack).gameObject;

        if(effect) {
            Vector3 originalRotation = effect.transform.eulerAngles;
            if(battleObjManager.attackerPositionStart.x > 0) {
                effect.transform.eulerAngles = new Vector3(
                    effect.transform.eulerAngles.x,
                    effect.transform.eulerAngles.y ,
                    effect.transform.eulerAngles.z + 180
                );
            }
            effect.SetActive(true);
            yield return new WaitForSeconds(2f);
            effect.SetActive(false);
            effect.transform.eulerAngles = originalRotation;
        }
        battleObjManager.battleSystemHud.RefreshAllHUDs();

        for(int i = 0; i < battleObjManager.allPlayers.Count; i++){
            if(battleSystemUtils.CheckPlayerDeadAndAnimate(battleObjManager.allPlayers[i]))
                battleObjManager.deadPlayerList.Add(battleObjManager.allPlayers[i]);
        }



        newState = new BattleStateAttackAnimationRetreat();
        yield return newState;
    }
}
