using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateSystem : MonoBehaviour
{
    private static StateSystem _instance;
    public static StateSystem instance { get { return _instance; } }    
    public Animator machine;

    void Awake()
    {
        _instance = this;
        machine = GetComponent<Animator>();
    }

    void Start()
    {
        if( SceneManager.GetActiveScene().name == "Battle" ) {
            Invoke("GoToBattle", 1f);
        }
    }

    void GoToBattle()
    {
        machine.SetTrigger("Battle");
    }

    void Update()
    {
        
    }
}
