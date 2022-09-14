using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicMenu : MonoBehaviour
{
    public GameObject _menu;
    public GameObject _menuItem;

    public GameObject menuList;

    public void Open(Dictionary<string, Action> items)
    {
        foreach( var item in items )
        {
            bool sub = item.Key.Contains(">>");
            AddItem(item.Key, item.Value, !(sub));
        }

        Canvas.ForceUpdateCanvases();            
    }

    public void Close()
    {
        Destroy(gameObject);
    }

    public void SubMenu(Dictionary<string, Action> items)
    {
        if( GameObject.Find("Menu(Clone)(Clone)") ) { return; }
        
        GameObject obj = Instantiate(_menu, transform.position, transform.rotation);
        obj.transform.SetParent(transform);
        obj.transform.position = transform.position + new Vector3(50, 0, 0);
  
        DynamicMenu menu = obj.GetComponent<DynamicMenu>();
        menu.RemoveItems();
        menu.Open(items);
    }    

    private void RemoveItems()
    {
        foreach (Transform child in transform.GetChild(0).transform)
        {
            if(child.GetComponent<DynamicMenuItem>()) {
                Destroy(child.gameObject);
            }
        }
    }

    private void AddItem(string text, Action callback, bool close = true)
    {
        GameObject obj = Instantiate(_menuItem, menuList.transform.position, menuList.transform.rotation);
        obj.transform.SetParent(menuList.GetComponent<Transform>());
        obj.transform.GetComponent<DynamicMenuItem>().SetLabel(text);
        obj.transform.GetComponent<DynamicMenuItem>().SetCallback(delegate { if(close) Close(); callback(); });
    }
}