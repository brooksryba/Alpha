using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyStart : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // @ todo - allow enemy to use items in battle
        BattleObjectManager _manager = BattleObjectManager.instance;
        ToastSystem.instance.Open(_manager.charManager.attackerName + " is about to attack!", false);
        animator.SetTrigger("BattleAttack");
    }
}
