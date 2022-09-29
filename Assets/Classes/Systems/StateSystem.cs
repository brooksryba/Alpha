using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSystem : MonoBehaviour
{
    Animator animator;

    void Awake(){
        animator = GetComponent<Animator>();
    }

    void Start()
    {
        Debug.Log(animator);
    }

    void Update()
    {
        
    }
}
