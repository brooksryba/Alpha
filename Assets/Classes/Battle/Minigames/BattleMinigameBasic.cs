using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BattleMinigameBasic : BattleMinigame
{
    public float timeLeft;
    public TextMeshProUGUI minigameText;
    public GameObject buttonPrefab;
    public GameObject buttonObj;
    public Button button;

    override public void setupMinigame(){
        minigameText = minigameTextObj.GetComponent<TextMeshProUGUI>();
        buttonPrefab = Resources.Load("Prefabs/MinigameButton") as GameObject;
        
    }

    override public void runMinigame() {
        timeLeft = 3.0f;
        buttonObj = GameObject.Instantiate(buttonPrefab, minigameText.transform);
        button = buttonObj.GetComponent<Button>();
        button.onClick.AddListener(buttonClick);

    }

    public void buttonClick() {
        completedSuccessfully = true;
        bonusMultiplier = 2.0;
    }

    override public void Update(){
        if(minigameInProgress){
            if(timeLeft > 0f){
                timeLeft -= Time.deltaTime;
            }
            else {
                timeLeft = 0f;
                minigameComplete = true;
                GameObject.Destroy(buttonObj);
            }
            updateTimer(timeLeft);
        }

    }

    void updateTimer(float currentTime){
        //Debug.Log("Made it to update timer " + currentTime.ToString("0.00"));
        minigameText.text = currentTime.ToString("0.0");
    }

    override public void breakdownMinigame(){
        minigameText.text = "";

    }
}
