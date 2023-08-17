using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateCutscene : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        int chapter = animator.GetInteger("cutsceneChapter");
        int mark = animator.GetInteger("cutsceneMark");

        TextAsset ink = Resources.Load("Dialogue/Chapters/Chapter_"+chapter+"_"+mark) as TextAsset;

        animator.SetBool("worldInCutscene", true);
        Debug.Log("State-cutscene Enter");
        CutsceneSystem.instance.EnterCutsceneMode();
        DialogSystem.instance.EnterDialogueMode(ink, (s) => {CutsceneSystem.instance.HandleCutsceneEvent(s);}, () => {animator.ResetTrigger("Cutscene"); animator.SetBool("worldInCutscene", false);});

        if(SceneSystem.battle != null && SceneSystem.battle.scenePath != null && SceneSystem.battle.scenePath != "") {
            Debug.Log("Scene System - Scene Path");
            Debug.Log(SceneSystem.battle.scenePath);
            DialogSystem.instance.SetStoryCurrentPath(SceneSystem.battle.scenePath);
            SceneSystem.battle.scenePath = null;
        }

        DialogSystem.instance.ContinueStory();       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bool worldInBattle = animator.GetBool("worldInBattle");

        if( CutsceneSystem.instance != null )
            CutsceneSystem.instance.ExitCutsceneMode();
        if( StorySystem.instance != null && !worldInBattle)
            StorySystem.instance.MoveToNextMark();       
    }
}
