using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class App : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Run", 5);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Maintain the application object for the session
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Initialize the first scene
    void Run()
    {
        SceneManager.LoadScene (sceneName:"Menu");
    }
}
