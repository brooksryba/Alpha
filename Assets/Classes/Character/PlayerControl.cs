using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if( Input.GetKeyUp(KeyCode.Q) ) {
            transform.GetComponent<Player>().SaveState();
        }
        if( Input.GetKeyUp(KeyCode.R) ) {
            SaveSystem.Reset();
            SceneManager.LoadScene("World");
        }
        if( Input.GetKeyUp(KeyCode.O) ) {
            GameObject obj = Instantiate(Resources.Load("Menu"), transform.position, transform.rotation) as GameObject;
            DynamicMenu menu = obj.GetComponent<DynamicMenu>();
            menu.Open(new Dictionary<string, Action>(){
                {"Player", () => Debug.Log("player")},
                {"Items", () => Debug.Log("items")},
                {"Map", () => Debug.Log("map")},
                {"Save", () => Debug.Log("save")},
                {"Quit", () => Debug.Log("quit")},
            });
        } 
    }
}
