using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitch : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSavedScene()
    {
        PlayerLocationData data = SaveSystem.LoadState<PlayerLocationData>("PlayerLocation") as PlayerLocationData;
        if( data != null ) {
            SceneManager.LoadScene(data.scene);
        }        
    }    

    public void ResetScenes()
    {
        SaveSystem.Reset();
    }    

}
