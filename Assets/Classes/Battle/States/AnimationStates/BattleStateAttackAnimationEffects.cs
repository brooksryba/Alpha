using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStateAttackAnimationEffects : BattleState
{

    override public IEnumerator execute()
    {
        newState = this;
        GameObject effect;

        if(_manager.charManager.attacker.transform.GetChild(0).Find(_manager.chosenBattleMove))
            effect = _manager.charManager.attacker.transform.GetChild(0).Find(_manager.chosenBattleMove).gameObject;
        else 
            effect = _manager.charManager.attacker.transform.GetChild(0).Find("Basic Attack").gameObject;

        if(effect) {
            Vector3 originalRotation = effect.transform.eulerAngles;
            if(_manager.charManager.originalPositions[_manager.charManager.attackerName].x > 0) {
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
        _manager.battleSystemHud.RefreshAllHUDs();

        // animate players who have died
        for(int i = 0; i < _manager.charManager.allPlayers.Count; i++){
            if(battleSystemUtils.CheckPlayerDeadAndAnimate(_manager.charManager.allPlayers[i]))
                _manager.charManager.deadPlayerList.Add(_manager.charManager.allPlayers[i]);
        }        

        newState = new BattleStateAttackAnimationRetreat();
        yield return newState;
    }
}
