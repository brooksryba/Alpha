using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateAttackMinigame : BattleState
{
    public bool minigameStarted = false;
    public bool keepMinigameRunning = true;
    public GameObject minigamePrefab;
    public GameObject minigameObj;
    public GameObject minigameContainer;
    public BattleMinigameData ongoingMinigameData;

    override public IEnumerator execute()
    {
        newState = this;
        
        if(!minigameStarted){
            
            minigameStarted = true;
            string minigameName = battleSystemUtils.GetMinigameNameFromAttack(battleObjManager.chosenAttack);

            if(minigameName != null & minigameName != ""){
                battleObjManager.dialogueText.text = "Complete the Minigame for extra Attack!";
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
            battleSystemUtils.DoAttack(battleObjManager.chosenAttack, battleObjManager.playerUnit, battleObjManager.enemyUnit, ongoingMinigameData.bonusMultiplier);

            battleObjManager.attacker = GameObject.Find(battleObjManager.playerUnit.title);
            battleObjManager.defender = null;
            if(battleObjManager.enemyUnit) battleObjManager.defender = GameObject.Find(battleObjManager.enemyUnit.title);
            
            if(ongoingMinigameData.completedSuccessfully){
                battleObjManager.dialogueText.text = "The attack is successful with extra damage!";
            } else {
                battleObjManager.dialogueText.text = "The attack is successful";
            }


            newState = new BattleStateAttackAnimationApproach();
            


        }

        yield return new WaitForSeconds(0f);
    }
}
