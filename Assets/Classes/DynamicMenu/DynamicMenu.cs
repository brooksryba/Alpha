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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Open(Dictionary<string, Action> items)
    {
        foreach( var item in items )
        {
            AddItem(item.Key, item.Value);
        }

        Canvas.ForceUpdateCanvases();            
    }

    public void Close()
    {
        Destroy(gameObject);
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

    private void AddSubmenu(string text)
    {
        AddItem(text, delegate{ SpawnMenu(); }, false);
    }
    private void SpawnMenu()
    {
        GameObject obj = Instantiate(_menu, transform.position, transform.rotation);
        obj.transform.SetParent(transform);
        obj.transform.position = transform.position + new Vector3(50, 0, 0);
  
        DynamicMenu menu = obj.GetComponent<DynamicMenu>();
        menu.RemoveItems();
        menu.AddItem("item 1", () => Debug.Log("item1"));
        menu.AddSubmenu("test");
    }
}