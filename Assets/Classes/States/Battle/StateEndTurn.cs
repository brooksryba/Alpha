using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEndTurn : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        BattleSystemUtils battleSystemUtils = new BattleSystemUtils();

        _manager.battleBonusManager.IncrementPlayerTurn(_manager.condition.attacker.GetComponent<Character>().title);
        _manager.battleSystemHud.RefreshAllHUDs();
         _manager.battleSystemHud.selection = null;
        _manager.chosenBattleMove = "";

        animator.ResetTrigger("BattleAttack");
        animator.ResetTrigger("BattleItem");
        animator.ResetTrigger("BattleInvalidSelection");
        animator.ResetTrigger("BattleSelection");
        animator.ResetTrigger("BattleMinigame");
        animator.ResetTrigger("BattleApproach");
        animator.ResetTrigger("BattleEffects");
        animator.ResetTrigger("BattleRetreat");
        animator.ResetTrigger("BattleResign");
        animator.SetBool("battleOver", battleSystemUtils.PartyDead(_manager.condition.enemyParty) || battleSystemUtils.PartyDead(_manager.condition.playerParty));        
    }
}
