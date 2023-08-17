using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateReturn : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   SaveSystem.SaveAndDeregister();
        Debug.Log("Battle Scene Change");
        animator.SetBool("worldInBattle", false);
        SceneManager.LoadScene(sceneName: SceneSystem.battle.scene);                 
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    
}
