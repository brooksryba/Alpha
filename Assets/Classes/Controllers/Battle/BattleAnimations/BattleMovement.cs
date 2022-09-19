using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleMovement : MonoBehaviour
{
    public Vector3 positionStart;
    public Vector3 positionEnd;
    public bool isAnimating = false;
    public bool isFinished = false;


    void Update(){
        if(isAnimating){
            if(Vector3.Distance(transform.position, positionEnd) <= 0.01f) {
                isAnimating = false;
                isFinished = true;
            }
        }
    }

    public void Animate(Vector3 end){
        positionStart = transform.position;
        positionEnd = end;
        gameObject.GetComponent<CharacterMovement>().targetLocations.Add(end);
        isAnimating = true;
    }

    public void Reset() {
        isAnimating = false;
        isFinished = false;
    }

}


