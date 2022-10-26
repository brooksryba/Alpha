using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSetup : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager.instance.battleSystemHud.RefreshAllHUDs();
        ToastSystem.instance.Open(BattleObjectManager.instance.condition.enemyParty[0] + " engages in battle...", false);
    }
}
