using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateFinish : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        BattleSystemUtils battleSystemUtils = new BattleSystemUtils();

        if(battleSystemUtils.PartyDead(_manager.charManager.enemyParty))
        {
            ToastSystem.instance.Open("You won the battle!", false);

            // @TODO, currently it splits total xp into thirds and distributed evenly, will need to confirm 
            int totalXp = 0;
            foreach(string characterName in _manager.charManager.enemyParty){
                Character enemyCharacter = battleSystemUtils.GetCharacter(characterName);
                totalXp += enemyCharacter.earnedXp;
            }
            int xpToAdd = Mathf.FloorToInt(totalXp / 3);

            if(xpToAdd > 0){
                foreach(string characterName in _manager.charManager.playerParty){
                    ToastSystem.instance.Open(characterName + " earned " + xpToAdd.ToString() + " EXP!");
                    Character playerCharacter = battleSystemUtils.GetCharacter(characterName);
                    playerCharacter.AddXp(xpToAdd);
                    _manager.battleSystemHud.RefreshAllHUDs();
                }

            }
        }
        else if (battleSystemUtils.PartyDead(_manager.charManager.playerParty))
        {
            ToastSystem.instance.Open("You were defeated", false);
        } 

        _manager.battleBonusManager.DestroyAllBonuses();
        SaveSystem.SaveAndDeregister();
        SceneManager.LoadScene(sceneName: SceneSystem.battle.scene);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
