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

    override public IEnumerator execute()
    {
        newState = this;
        
        // this will always execute when the state is reached
        if(!minigameStarted){
            
            minigameStarted = true;
            isEnemyTurn = _manager.charManager.enemyParty.Contains(_manager.charManager.attackerName);

            string minigameName = battleSystemUtils.GetMinigameNameFromBattleMove(_manager.chosenBattleMove, isEnemyTurn);

            if(minigameName != null & minigameName != ""){
                if(isEnemyTurn) _manager.dialogueText.text = "Complete the Minigame to boost your defense!";
                else _manager.dialogueText.text = "Complete the Minigame to boost your attack!";
                minigamePrefab = Resources.Load("Prefabs/Battle/Minigames/" + minigameName) as GameObject;
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
            battleSystemUtils.ExecuteBattleMove(_manager.chosenBattleMove, 
                                       battleSystemUtils.GetCharacter(_manager.charManager.attackerName), 
                                       battleSystemUtils.GetCharacter(_manager.charManager.defenderName), 
                                       isEnemyTurn ? 1.0f / ongoingMinigameData.bonusMultiplier: ongoingMinigameData.bonusMultiplier,
                                       ongoingMinigameData.completedSuccessfully);

            BattleMoveBase chosenMoveDetails = battleSystemUtils.PrepChosenBattleMove(_manager.chosenBattleMove,
                    battleSystemUtils.GetCharacter(_manager.charManager.attackerName), battleSystemUtils.GetCharacter(_manager.charManager.defenderName));

            if(ongoingMinigameData.completedSuccessfully){
                if(isEnemyTurn) _manager.dialogueText.text = "The " + chosenMoveDetails.moveType + " " + chosenMoveDetails.moveName + " is successful, but you decreased it's effect!";
                else _manager.dialogueText.text = "The " + chosenMoveDetails.moveType + " " + chosenMoveDetails.moveName + " is successful with an increased effect!";
            } else {
                _manager.dialogueText.text = "The " + chosenMoveDetails.moveType + " " + chosenMoveDetails.moveName + " is successful!";
            }

            if(chosenMoveDetails.moveType=="Attack")
                newState = new BattleStateAttackAnimationApproach();
            else 
                newState = new BattleStateSpellAnimationApproach();
            


        }

        yield return new WaitForSeconds(0f);
    }
}
