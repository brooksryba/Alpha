using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSpriteController : MonoBehaviour
{
    // focus right now is to transition between colors, but other things are possible
    public bool isTransitioning = false;
    public bool isFinished = false;

    public SpriteRenderer sprite;
    public Color startColor;
    public Color EndColor;
    public float timeRemaining;
    public float timeToTransition;

    

    void Update(){
        if(isTransitioning){
            timeRemaining -= Time.deltaTime;
            float percentTimeElapsed = (timeToTransition - timeRemaining) / timeToTransition;
            sprite.color = new Color(startColor.r + (float)percentTimeElapsed*(EndColor.r - startColor.r), 
                                     startColor.g + (float)percentTimeElapsed*(EndColor.g - startColor.g), 
                                     startColor.b + (float)percentTimeElapsed*(EndColor.b - startColor.b), 
                                     startColor.a + (float)percentTimeElapsed*(EndColor.a - startColor.a));
            if(timeRemaining<=0.0){
                sprite.color = EndColor;
                isTransitioning = false;
                isFinished = true;
            }
        }
    }

    public void TransitionColors(Color start, Color end, float timeInSeconds=3.0f){
        startColor = start;
        EndColor = end;
        if(timeInSeconds > 0.0f){
            timeRemaining = timeInSeconds;
            timeToTransition = timeInSeconds;
        } else {
            timeRemaining = 1.0f;
            timeToTransition = 1.0f;   
        }

        isTransitioning = true;
    }

    public void Reset() {
        isTransitioning = false;
        isFinished = false;
    }
}


