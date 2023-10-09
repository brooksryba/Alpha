using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateSystem : MonoBehaviour
{
    public static StateSystem instance { get; private set; }    
    private void OnEnable() { instance = this; }

    public Animator machine;
    public bool reset = false;

    void Awake()
    {
        machine = GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    public void Reset()
    {
        reset = true;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Battle" ){
            machine.SetBool("newGame", false);
        }
        else if( StorySystem.instance != null && StorySystem.instance.chapter == 1 && StorySystem.instance.mark == 1 ) {
            machine.SetBool("newGame", true);
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

    public void SetInteger(string title, int value)
    {
        machine.SetInteger(title, value);
    }
}
