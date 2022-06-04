using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleMinigame
{

    public bool completedSuccessfully = false; // set to true if player wins
    public bool minigameComplete = false; // needs to be set to true to continue the battle

    public bool minigameInProgress = false;
    public double bonusMultiplier = 1.0; // multiplier will be handled by the attack/spell/etc
    public GameObject minigameTextObj = GameObject.Find("MinigameText");
    public BattleMinigame(){
        MinigameUpdateCaller.OnUpdate += Update;
    }

    ~BattleMinigame()
    {
        MinigameUpdateCaller.OnUpdate -= Update;
    }


    virtual public void setupMinigame(){
    }

    virtual public void breakdownMinigame(){
    }

    virtual public void runMinigame() {
        this.minigameComplete = true;
    }

    virtual public void Update() {
    }


    public void resetMinigame(){
        this.completedSuccessfully = false; // set to true if player wins
        this.minigameComplete = false; // needs to be set to true to continue the battle

        this.minigameInProgress = false;
        this.bonusMultiplier = 1.0; // multiplier will be handled by the attack/spell/etc
    }


}
