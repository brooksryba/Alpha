using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSystem : MonoBehaviour
{
    public List<GameObject> menus;

    public void ShowMenu(string menuName)
    {
        foreach(GameObject go in menus) {
            go.SetActive(go.name == menuName);
        }
    }
    
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSavedScene()
    {
        PlayerLocationData data = SaveSystem.instance.LoadState<PlayerLocationData>("PlayerLocation") as PlayerLocationData;
        if( data != null ) {
            SceneManager.LoadScene(data.scene);
        }        
    }    

    public void ResetScenes()
    {
        SaveSystem.instance.Reset();
    }    

    public void Quit()
    {
        Application.Quit();
    }
}
