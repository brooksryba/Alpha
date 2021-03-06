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
        if( Input.GetKeyUp(KeyCode.F1) ) {
            SaveSystem.ResetAndDeregister();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if( Input.GetKeyUp(KeyCode.F2) ) {
            SaveSystem.Save();
            GameObject.Find("ToastSystem").GetComponent<ToastSystem>().Open("Saving...");
        }        
        if( Input.GetKeyUp(KeyCode.E) ) {
            GameObject.Find("Player").GetComponent<PlayerMovement>().HandleCollisionInteraction();
        }        
        if( Input.GetKeyUp(KeyCode.O) ) {
            if( GameObject.Find("Menu(Clone)") ) { return; }
            
            GameObject obj = Instantiate(Resources.Load("Prefabs/Menu"), transform.position, transform.rotation) as GameObject;
            Character character = GameObject.Find("Player").GetComponent<Character>();
            DynamicMenu menu = obj.GetComponent<DynamicMenu>();

            Dictionary<string, Action> items = new Dictionary<string, Action>();
            Dictionary<string, int> itemCount = character.GetInventoryItemCounts();
            Dictionary<string, ItemData> itemRefs = character.GetInventoryItemRefs();

            foreach( KeyValuePair<string, int> item in itemCount )
            {
                items.Add(item.Key + " (x" + item.Value + ")", () => {
                    if( WorldItems.lookup.ContainsKey(item.Key) )
                    {
                        InventoryItemData itemData = WorldItems.lookup[item.Key];
                        Character character = GameObject.Find("Player").GetComponent<Character>();
                        string message = itemData.Execute(character);

                        GameObject.Find("ToastSystem").GetComponent<ToastSystem>().Open(message);
                        Destroy(GameObject.Find("Menu(Clone)"));
                    } 
                    else
                    {
                        GameObject.Find("ToastSystem").GetComponent<ToastSystem>().Open("Can't use this item now.");
                    }
                });
            }
            items.Add("Return", () => {});

            menu.Open(new Dictionary<string, Action>(){
                {"Player", () => {
                    Character player = GameObject.Find("Player").GetComponent<Character>();
                    string message = "HP: " + player.currentHP + "/" + player.characterClass.maxHP + "\nMana: " + player.currentMana + "/" + player.characterClass.maxMana;
                    GameObject.Find("ToastSystem").GetComponent<ToastSystem>().Open(message);
                }},
                {"Items >>", delegate { menu.SubMenu(items); }},
                {"Map", () => {
                    GameObject.Find("ToastSystem").GetComponent<ToastSystem>().Open("Not implemented.");
                }},
                {"Save", () => {
                    SaveSystem.Save();
                    GameObject.Find("ToastSystem").GetComponent<ToastSystem>().Open("Saving...");
                }},
                {"Quit", () => SceneManager.LoadScene("Menu")},
                {"Return", () => {}},
            });
        } 
    }
   

}

