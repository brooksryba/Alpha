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
        else if (_manager.chosenMoveDetails.moveType == "Spell") {
            effect = _manager.charManager.attacker.transform.GetChild(0).Find("Spell").gameObject;
            var partSystemMain = effect.GetComponent<ParticleSystem>().main;
            Vector3 currentPosition = GameObject.Find(_manager.charManager.attackerName).transform.position;
            if (!_manager.charManager.defender || (_manager.charManager.defender==_manager.charManager.attacker)){
                effect.transform.eulerAngles = new Vector3(effect.transform.eulerAngles.x, effect.transform.eulerAngles.y, 90);
                partSystemMain.startSpeed = 0;
                
            }
            else {
                float targetAngle = Mathf.Rad2Deg*Mathf.Atan((_manager.charManager.defender.transform.position.y - currentPosition.y) / 
                    (_manager.charManager.defender.transform.position.x - currentPosition.x));
                    if(currentPosition.x > _manager.charManager.defender.transform.position.x)
                        targetAngle = targetAngle + 180.0f;
                effect.transform.eulerAngles = new Vector3(effect.transform.eulerAngles.x, effect.transform.eulerAngles.y, targetAngle);
                partSystemMain.startSpeed = 3;
            }

        }
        else 
            effect = _manager.charManager.attacker.transform.GetChild(0).Find("Basic Attack").gameObject;

        if(effect) {
            Vector3 originalRotation = effect.transform.eulerAngles;
            if(_manager.charManager.originalPositions[_manager.charManager.attackerName].x > 0 && _manager.chosenMoveDetails.moveType != "Spell") {
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
