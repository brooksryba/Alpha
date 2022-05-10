using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSwitch : MonoBehaviour
{
    public List<GameObject> menus;

    public void ShowMenu(string menuName)
    {
        foreach(GameObject go in menus) {
            go.SetActive(go.name == menuName);
        }
    }

    public void Quit()
    {
        Application.Quit();
    }
}
