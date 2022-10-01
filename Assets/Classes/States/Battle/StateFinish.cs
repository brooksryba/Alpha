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
            ToastSystem.instance.Queue("You won the battle!");

            // @TODO, currently it splits total xp into thirds and distributed evenly, will need to confirm 
            int totalXp = 0;
            foreach(string characterName in _manager.charManager.enemyParty){
                Character enemyCharacter = battleSystemUtils.GetCharacter(characterName);
                totalXp += enemyCharacter.earnedXp;
            }
            int xpToAdd = Mathf.FloorToInt(totalXp / 3);

            if(xpToAdd > 0){
                foreach(string characterName in _manager.charManager.playerParty){
                    ToastSystem.instance.Queue(characterName + " earned " + xpToAdd.ToString() + " EXP!");
                    Character playerCharacter = battleSystemUtils.GetCharacter(characterName);
                    playerCharacter.AddXp(xpToAdd);
                    _manager.battleSystemHud.RefreshAllHUDs();
                }

            }
        }
        else if (battleSystemUtils.PartyDead(_manager.charManager.playerParty))
        {
            ToastSystem.instance.Queue("You were defeated");
        }
        else if (_manager.playerResigned == true) 
        {
            ToastSystem.instance.Queue("You resigned!");
        }

        _manager.battleBonusManager.DestroyAllBonuses();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(ToastSystem.instance.complete) {
            animator.ResetTrigger("BattleAttack");
            animator.ResetTrigger("BattleItem");
            animator.ResetTrigger("BattleInvalidSelection");
            animator.ResetTrigger("BattleSelection");
            animator.ResetTrigger("BattleMinigame");
            animator.ResetTrigger("BattleApproach");
            animator.ResetTrigger("BattleEffects");
            animator.ResetTrigger("BattleRetreat");
            animator.ResetTrigger("BattleResign");       
            animator.SetTrigger("BattleText");
            animator.SetBool("battleOver", true);
            animator.SetBool("battleSkipTurn", false);
            animator.SetBool("worldInBattle", false);

            SaveSystem.SaveAndDeregister();
            SceneManager.LoadScene(sceneName: SceneSystem.battle.scene);            
        }
    }    
}
