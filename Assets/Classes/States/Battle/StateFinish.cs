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

        if(battleSystemUtils.PartyDead(_manager.condition.enemyParty))
        {
            ToastSystem.instance.Queue("You won the battle!");

            // @TODO, currently it splits total xp into thirds and distributed evenly, will need to confirm 
            int totalXp = 0;
            foreach(string characterID in _manager.condition.enemyParty){
                totalXp += CharacterManager.refs[characterID].condition.xp;
            }
            int xpToAdd = Mathf.FloorToInt(totalXp / 3);

            if(xpToAdd > 0){
                foreach(string characterID in _manager.condition.playerParty){
                    ToastSystem.instance.Queue(CharacterManager.refs[characterID].title + " earned " + xpToAdd.ToString() + " EXP!");

                    CharacterManager.refs[characterID].condition.xp += xpToAdd;
                    int newLevel = LevelSystem.GetLevel(CharacterManager.refs[characterID].condition.xp);
                    for(int lvl = CharacterManager.refs[characterID].condition.level; lvl < newLevel; lvl++){
                        CharacterManager.refs[characterID].condition.level += 1;
                        ToastSystem.instance.Queue(CharacterManager.refs[characterID].title + " is now level " + CharacterManager.refs[characterID].condition.level.ToString() + "!");

                        CharacterManager.SetStats(characterID);
                        // TODO - Level Progression is not set up currently
                        // if(playerCharacter.characterClass.attackProgression.ContainsKey(playerCharacter.level)){
                        //     if(!playerCharacter.attackNames.Contains(playerCharacter.characterClass.attackProgression[playerCharacter.level])){
                        //         playerCharacter.attackNames.Add(playerCharacter.characterClass.attackProgression[playerCharacter.level]);
                        //         ToastSystem.instance.Queue(this.name + " has learned attack " + playerCharacter.characterClass.attackProgression[playerCharacter.level] + "!");
                        //     }
                        // }

                        // if(playerCharacter.characterClass.spellProgression.ContainsKey(playerCharacter.level)){
                        //     if(!playerCharacter.spellNames.Contains(playerCharacter.characterClass.spellProgression[playerCharacter.level])){
                        //         playerCharacter.spellNames.Add(playerCharacter.characterClass.spellProgression[playerCharacter.level]);
                        //         ToastSystem.instance.Queue(playerCharacter.name + " has learned spell " + playerCharacter.characterClass.spellProgression[playerCharacter.level] + "!");
                        //     }
                        // }

                    }

                    _manager.battleSystemHud.RefreshAllHUDs();
                }

            }
        }
        else if (battleSystemUtils.PartyDead(_manager.condition.playerParty))
        {
            ToastSystem.instance.Queue("You were defeated");
        }
        else if (_manager.playerResigned == true) 
        {
            ToastSystem.instance.Queue("You resigned!");
        }

        _manager.DestroyAllBattleEffects();
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
            animator.SetBool("battleOver", true);
            animator.SetBool("battleSkipTurn", false);
            animator.SetBool("worldInBattle", false);
            animator.SetTrigger("SceneChange");         
        }
    }    
}
