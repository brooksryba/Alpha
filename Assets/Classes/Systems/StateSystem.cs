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
        if( true) {
            machine.SetBool("newGame", true);
        }
        if( SceneManager.GetActiveScene().name == "Battle" ) {
            Invoke("GoToBattle", 1f);
        }
    }

    public void Trigger(string title)
    {
        machine.SetTrigger(title);
    }

    public void Reset(string title)
    {
        machine.ResetTrigger(title);
    }

    public void SetBool(string title, bool value)
    {
        machine.SetBool(title, value);
    }

    void GoToBattle()
    {
        machine.SetTrigger("Battle");
    }

    void Update()
    {
        
    }
}
