using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCutscene : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int chapter = StorySystem.instance.chapter;
        int mark = StorySystem.instance.mark;

        TextAsset ink = Resources.Load("Dialogue/Chapters/Chapter_"+chapter+"_"+mark) as TextAsset;

        CutsceneSystem.instance.EnterCutsceneMode();
        DialogSystem.instance.EnterDialogueMode(ink, (s) => {}, () => {animator.ResetTrigger("Cutscene"); animator.SetBool("worldInCutscene", false);});
        DialogSystem.instance.ContinueStory();       
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        CutsceneSystem.instance.ExitCutsceneMode();
        StorySystem.instance.MoveToNextMark();       
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
