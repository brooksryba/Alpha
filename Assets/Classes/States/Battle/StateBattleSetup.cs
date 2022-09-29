using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSetup : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager.instance.battleSystemHud.RefreshAllHUDs();
        ToastSystem.instance.Open(BattleObjectManager.instance.charManager.enemyParty[0] + " engages in battle...", false);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }
}
