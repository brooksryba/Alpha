using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateAttackMinigame : BattleState
{
    public bool minigameStarted = false;
    public bool keepMinigameRunning = true;
    public bool isEnemyTurn;
    public GameObject minigamePrefab;
    public GameObject minigameObj;
    public GameObject minigameContainer;
    public BattleMinigameData ongoingMinigameData;
    public string attackerName;
    public string defenderName;

    override public IEnumerator execute()
    {
        newState = this;
        
        // this will always execute when the state is reached
        if(!minigameStarted){
            
            minigameStarted = true;
            attackerName = battleObjManager.attacker.GetComponent<Character>().title;
            if(battleObjManager.defender) defenderName = battleObjManager.defender.GetComponent<Character>().title;
            isEnemyTurn = battleObjManager.enemyParty.Contains(attackerName);

            string minigameName = battleSystemUtils.GetMinigameNameFromBattleMove(battleObjManager.chosenBattleMove, isEnemyTurn);

            if(minigameName != null & minigameName != ""){
                if(isEnemyTurn) battleObjManager.dialogueText.text = "Complete the Minigame to boost your defense!";
                else battleObjManager.dialogueText.text = "Complete the Minigame to boost your attack!";
                minigamePrefab = Resources.Load("Prefabs/BattleMinigames/" + minigameName) as GameObject;
                minigameContainer = GameObject.Find("BattleMinigameContainer");
                minigameObj = GameObject.Instantiate(minigamePrefab, minigameContainer.transform);
                minigameObj.transform.SetParent(minigameContainer.transform);
                ongoingMinigameData = minigameObj.GetComponent<BattleMinigameBase>().minigameData;

            } else {
                ongoingMinigameData = new BattleMinigameData();
                ongoingMinigameData.minigameComplete = true;
                ongoingMinigameData.completedSuccessfully = false;
                keepMinigameRunning = false;

            }
        }

        if(ongoingMinigameData.minigameComplete){
            yield return new WaitForSeconds(1f);
            if(minigameObj != null)
                GameObject.Destroy(minigameObj);
            battleSystemUtils.ExecuteBattleMove(battleObjManager.chosenBattleMove, 
                                       battleSystemUtils.GetCharacter(attackerName), 
                                       battleSystemUtils.GetCharacter(defenderName), 
                                       isEnemyTurn ? 1.0f / ongoingMinigameData.bonusMultiplier: ongoingMinigameData.bonusMultiplier,
                                       ongoingMinigameData.completedSuccessfully);


            string moveType = battleSystemUtils.PrepChosenBattleMove(battleObjManager.chosenBattleMove, battleSystemUtils.GetCharacter(attackerName), battleSystemUtils.GetCharacter(defenderName)).moveType;
            if(ongoingMinigameData.completedSuccessfully){
                if(isEnemyTurn) battleObjManager.dialogueText.text = "The " + moveType + " is successful, but you increase your defense!";
                else battleObjManager.dialogueText.text = "The " + moveType + " is successful with boosted power!";
            } else {
                battleObjManager.dialogueText.text = "The " + moveType + " is successful!";
            }


            newState = new BattleStateAttackAnimationApproach();
            


        }

        yield return new WaitForSeconds(0f);
    }
}
