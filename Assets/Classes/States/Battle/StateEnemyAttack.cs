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

        (int targetID, Move aiChosenMove) = attackChooser.GetAttack(_manager.condition.attackerName);
        Character targetCharacter = CharacterManager.Get(targetID);

        _manager.chosenMove = aiChosenMove;

        if(targetID > 0)
            newMessage = _manager.condition.attackerName + " attacks " + targetCharacter.title + " with " + _manager.chosenMove.title + "!";
        else 
            newMessage = _manager.condition.attackerName + " uses " + _manager.chosenMove.title + "!";

        _manager.SetDefender(targetCharacter.title);
       
        ToastSystem.instance.Open(newMessage, false);       
        animator.SetTrigger("BattleAttack");
    }
}
