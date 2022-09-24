using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;



public class BattleStateEnd : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;

        if(battleSystemUtils.PartyDead(_manager.charManager.enemyParty))
        {
            newMessage = "You won the battle!";

            // @TODO, currently it splits total xp into thirds and distributed evenly, will need to confirm 
            int totalXp = 0;
            foreach(string characterName in _manager.charManager.enemyParty){
                Character enemyCharacter = battleSystemUtils.GetCharacter(characterName);
                totalXp += enemyCharacter.earnedXp;
            }
            int xpToAdd = Mathf.FloorToInt(totalXp / 3);

            yield return new WaitForSeconds(1f); 
            if(xpToAdd > 0){
                foreach(string characterName in _manager.charManager.playerParty){
                    ToastSystem.instance.Open(characterName + " earned " + xpToAdd.ToString() + " EXP!");
                    Character playerCharacter = battleSystemUtils.GetCharacter(characterName);
                    yield return new WaitForSeconds(1f);
                    yield return playerCharacter.AddXp(xpToAdd);
                    _manager.battleSystemHud.RefreshAllHUDs();
                }

            }




        } else if (battleSystemUtils.PartyDead(_manager.charManager.playerParty))
        {
            newMessage = "You were defeated";
        } 


        _manager.battleBonusManager.DestroyAllBonuses();

        yield return new WaitForSeconds(2f);

        SaveSystem.instance.SaveAndDeregister();
        SceneManager.LoadScene(sceneName: _manager.battleScriptable.scene);
        yield return new WaitForSeconds(0f);
    }
}
