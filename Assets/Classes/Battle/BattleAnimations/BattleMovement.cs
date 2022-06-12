using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    public Vector3 positionStart;

    public Vector3 positionEnd;
    public bool isAnimating = false;
    public bool isFinished = false;

    public float yComp; // ratio of y travel distance to x travel distance


    void Update(){
        if(isAnimating){
            Vector2 direction = new Vector2();
            
            direction.x = (positionStart.x > positionEnd.x ? -1 : (positionStart.x == positionEnd.x ? 0 : 1)); // left if -1, none if 0, right if 1
            direction.y = (positionStart.y > positionEnd.y ? -1 * yComp : (positionStart.y == positionEnd.y ? 0 : yComp)); // down if -1, none if 0, up if 1


            if(animator) {
                animator.SetFloat("Horizontal", direction.x);
                animator.SetFloat("Vertical", direction.y);
                animator.SetFloat("Speed", direction.sqrMagnitude);
            }

            if((direction.y > 0 ? rb.position.y >= positionEnd.y : rb.position.y <= positionEnd.y)) direction.y = 0;
            if((direction.x > 0 ? rb.position.x >= positionEnd.x : rb.position.x <= positionEnd.x)) direction.x = 0;

            if(direction.x != 0 || direction.y != 0) {   
                rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            } else {
                if(animator) {
                    animator.SetFloat("Horizontal", 0);
                    animator.SetFloat("Vertical", 0);
                    animator.SetFloat("Speed", 0);
                }
                isAnimating = false;
                isFinished  = true;
            }
        }
    }

    public void Animate(Vector3 start, Vector3 end){
        positionStart = start;
        positionEnd = end;
        yComp = 1 / (Math.Abs(positionEnd.x - positionStart.x) / Math.Abs(positionEnd.y - positionStart.y)); // ratio of y travel distance to x travel distance
        if(yComp <= 0.000001f) yComp = 0f; // arbitraily low non-zero numbers, like 2.507881E-08f, cause problems
        isAnimating = true;
    }

    public void Reset() {
        isAnimating = false;
        isFinished = false;
    }

}


