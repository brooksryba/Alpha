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

        // The first if/else block chooses the correct effect, and changes the target angle to point to correct defender
        if(_manager.condition.attacker.transform.GetChild(0).Find(_manager.chosenMove.moveID)){
            effect = _manager.condition.attacker.transform.GetChild(0).Find(_manager.chosenMove.moveID).gameObject;
        }
            
        else if (_manager.chosenMove.type == Move.Type.Spell) {
            effect = _manager.condition.attacker.transform.GetChild(0).Find("Spell").gameObject;
        }
        else {
            effect = _manager.condition.attacker.transform.GetChild(0).Find("Basic Attack").gameObject;
            }
            

        if(effect) {
            var partSystemMain = effect.GetComponent<ParticleSystem>().main;
            Vector3 originalRotation = effect.transform.eulerAngles;
            if(_manager.chosenMove.type == Move.Type.Spell){
                partSystemMain.startSpeed = 0;
                effect.transform.eulerAngles = new Vector3(effect.transform.eulerAngles.x, effect.transform.eulerAngles.y, 90.0f );
            } else {
                partSystemMain.startSpeed = 3;
                if(_manager.condition.originalPositions[_manager.condition.attackerID].x > 0){
                    effect.transform.eulerAngles = new Vector3(effect.transform.eulerAngles.x, effect.transform.eulerAngles.y, effect.transform.eulerAngles.z + 180.0f);
                }
            }

            effect.SetActive(true);
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
        for(int i = 0; i < _manager.condition.allPlayers.Count; i++){
            if(battleSystemUtils.CheckPlayerDeadAndAnimate(_manager.condition.allPlayers[i]))
                _manager.condition.deadPlayerList.Add(_manager.condition.allPlayers[i]);
        }            
    }
}
