using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMinigame : StateMachineBehaviour
{
    public bool keepMinigameRunning = true;
    public bool isEnemyTurn;
    public GameObject minigamePrefab;
    public GameObject minigameObj;
    public GameObject minigameContainer;
    public BattleMinigameData ongoingMinigameData;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        BattleSystemUtils battleSystemUtils = new BattleSystemUtils();

        isEnemyTurn = _manager.condition.enemyParty.Contains(_manager.condition.attackerName);

        string newMessage = "";
        string minigameName = battleSystemUtils.GetMinigameNameFromBattleMove(_manager.chosenMove, isEnemyTurn);

        if(minigameName != null & minigameName != ""){
            if(isEnemyTurn) newMessage = "Complete the Minigame to boost your defense!";
            else newMessage = "Complete the Minigame to boost your attack!";
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

        ToastSystem.instance.Open(newMessage, false);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BattleObjectManager _manager = BattleObjectManager.instance;
        BattleSystemUtils battleSystemUtils = new BattleSystemUtils();
        string newMessage = "";

        if(ongoingMinigameData.minigameComplete){
            if(minigameObj != null)
                GameObject.Destroy(minigameObj);
            battleSystemUtils.ExecuteBattleMove(_manager.chosenMove, 
                                       battleSystemUtils.GetCharacter(_manager.condition.attackerName), 
                                       battleSystemUtils.GetCharacter(_manager.condition.defenderName), 
                                       isEnemyTurn ? 1.0f / ongoingMinigameData.bonusMultiplier: ongoingMinigameData.bonusMultiplier,
                                       ongoingMinigameData.completedSuccessfully);


            if(ongoingMinigameData.completedSuccessfully){
                if(isEnemyTurn) newMessage = "The " + _manager.chosenMove.type.ToString() + " " + _manager.chosenMove.title + " is successful, but you decreased it's effect!";
                else newMessage = "The " + _manager.chosenMove.type.ToString() + " " + _manager.chosenMove.title + " is successful with an increased effect!";
            } else {
                newMessage = "The " + _manager.chosenMove.type.ToString() + " " + _manager.chosenMove.title + " is successful!";
            }

            ToastSystem.instance.Open(newMessage, false);
            animator.SetTrigger("BattleMinigame");           
        }              
    }
}
