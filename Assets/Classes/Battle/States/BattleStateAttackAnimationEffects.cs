using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStateAttackAnimationEffects : BattleState
{

    override public IEnumerator execute()
    {
        newState = this;
        GameObject effect;

        if(_manager.attacker.transform.GetChild(0).Find(_manager.chosenBattleMove))
            effect = _manager.attacker.transform.GetChild(0).Find(_manager.chosenBattleMove).gameObject;
        else 
            effect = _manager.attacker.transform.GetChild(0).Find("Basic Attack").gameObject;

        if(effect) {
            Vector3 originalRotation = effect.transform.eulerAngles;
            if(_manager.originalPositions[_manager.attackerName].x > 0) {
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
        for(int i = 0; i < _manager.allPlayers.Count; i++){
            if(battleSystemUtils.CheckPlayerDeadAndAnimate(_manager.allPlayers[i]))
                _manager.deadPlayerList.Add(_manager.allPlayers[i]);
        }        

        newState = new BattleStateAttackAnimationRetreat();
        yield return newState;
    }
}
