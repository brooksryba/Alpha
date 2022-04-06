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
            if( GameObject.Find("Menu(Clone)") ) { return; }
            
            GameObject obj = Instantiate(Resources.Load("Menu"), transform.position, transform.rotation) as GameObject;
            DynamicMenu menu = obj.GetComponent<DynamicMenu>();

            Dictionary<string, Action> items = new Dictionary<string, Action>();
            foreach( var item in transform.GetComponent<Player>().items )
            {
                items.Add(item.title, () => {});
            }
            items.Add("Return", () => {});

            menu.Open(new Dictionary<string, Action>(){
                {"Player", () => {}},
                {"Items", delegate { menu.SubMenu(items); }},
                {"Map", () => {}},
                {"Save", () => transform.GetComponent<Player>().SaveState()},
                {"Quit", () => SceneManager.LoadScene("Menu")},
                {"Return", () => {}},
            });
        } 
    }
}

