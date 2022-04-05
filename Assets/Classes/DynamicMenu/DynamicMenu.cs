using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMenu : MonoBehaviour
{
    public GameObject _menu;
    public GameObject _menuItem;

    public GameObject menuList;
    // Start is called before the first frame update
    void Start()
    {
        AddItem("Player", () => Debug.Log("player!"));
        AddSubmenu("Items");
        AddItem("Map", () => Debug.Log("map!"));
        AddItem("Save", () => Debug.Log("save!"));
        AddItem("Quit", () => Debug.Log("quit!"));
        Canvas.ForceUpdateCanvases();
        //obj.transform.SetParent(menuList.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Render()
    {
        // transform to a point on screen
        // populate MenuList with MenuItems
        // handle on click event for menu items
            // if submenu, spawn new menu
            // if not, send event
    }

    private void AddItem(string text, Action callback)
    {
        GameObject obj = Instantiate(_menuItem, menuList.transform.position, menuList.transform.rotation);
        obj.transform.SetParent(menuList.GetComponent<Transform>());
        obj.transform.GetComponent<DynamicMenuItem>().SetLabel(text);
        obj.transform.GetComponent<DynamicMenuItem>().SetCallback(callback);
    }

    private void SpawnMenu()
    {
        Debug.Log("It worked!");
    }

    private void AddSubmenu(string text)
    {
        AddItem(text, delegate{ SpawnMenu(); });
    }
}
