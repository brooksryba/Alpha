using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNewGame : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetInteger("cutsceneChapter", 1);
        animator.SetInteger("cutsceneMark", 1);
        animator.SetBool("cutsceneAdvance", true);
    }
}
