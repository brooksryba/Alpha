using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEffects : StateMachineBehaviour
{
    GameObject effect;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        BattleObjectManager _manager = BattleObjectManager.instance;

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
            effect.transform.eulerAngles = originalRotation;
        }
        _manager.battleSystemHud.RefreshAllHUDs();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
     animator.SetTrigger("BattleEffects");  
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        BattleSystemUtils battleSystemUtils = new BattleSystemUtils();

        if(effect)
            effect.SetActive(false);

        // animate players who have died
        for(int i = 0; i < _manager.charManager.allPlayers.Count; i++){
            if(battleSystemUtils.CheckPlayerDeadAndAnimate(_manager.charManager.allPlayers[i]))
                _manager.charManager.deadPlayerList.Add(_manager.charManager.allPlayers[i]);
        }            
    }
}
