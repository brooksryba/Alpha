using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateEnemyAttack : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        EnemyAttackChooser attackChooser = new EnemyAttackChooser();
        string newMessage = "";

        List<string> chosenAttackList = attackChooser.GetAttack(_manager.charManager.attackerName);

        _manager.chosenBattleMove = chosenAttackList[0];

        if(chosenAttackList[1] != "")
            newMessage = _manager.charManager.attackerName + " attacks " + chosenAttackList[1] + " with " + _manager.chosenBattleMove + "!";
        else 
            newMessage = _manager.charManager.attackerName + " uses " + _manager.chosenBattleMove + "!";

        _manager.SetDefender(chosenAttackList[1]);

        if(chosenAttackList[1] != "") _manager.charManager.defender = GameObject.Find(chosenAttackList[1]);
        
        ToastSystem.instance.Open(newMessage, false);       
        animator.SetTrigger("BattleAttack");
    }
}
