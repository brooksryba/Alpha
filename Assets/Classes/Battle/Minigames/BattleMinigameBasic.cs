using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BattleMinigameBasic : BattleMinigameBase
{
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI targetButtonText;
    public Button button;

    public int clickCounter = 0;
    public int numberClicksForWin = 3;
    public float timeLeft;


    public void Start(){
        setupMinigame();
    }
    public void setupMinigame() {
        timeLeft = 3.0f;
        button.onClick.AddListener(buttonClick);
        targetButtonText.text = numberClicksForWin.ToString();
    }

    public void buttonClick() {
        clickCounter += 1;
        if(numberClicksForWin - clickCounter > 0){
            targetButtonText.text = (numberClicksForWin - clickCounter).ToString();
        } else {
            targetButtonText.text = "WIN!";
            button.interactable = false;
            timeLeft = 0f;
        }
            

    }

    void updateTimer(float currentTime){
        timerText.text = "Click the button before the time runs out: " + currentTime.ToString("0.0");
    }

    public void Update(){
            if(timeLeft > 0f){
                timeLeft -= Time.deltaTime;
            }
            else {
                timeLeft = 0f;
                button.interactable = false;
                if(clickCounter >= numberClicksForWin){
                    minigameData.completedSuccessfully = true;
                    minigameData.bonusMultiplier = 2.0;
                }
                minigameData.minigameComplete = true;
            }
            updateTimer(timeLeft);
        
    }

}
