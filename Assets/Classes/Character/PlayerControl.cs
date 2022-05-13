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
        if( Input.GetKeyUp(KeyCode.R) ) {
            SaveSystem.Reset();
            SceneManager.LoadScene("World");
        }
        if( Input.GetKeyUp(KeyCode.O) ) {
            if( GameObject.Find("Menu(Clone)") ) { return; }
            
            GameObject obj = Instantiate(Resources.Load("Prefabs/Menu"), transform.position, transform.rotation) as GameObject;
            DynamicMenu menu = obj.GetComponent<DynamicMenu>();

            Dictionary<string, Action> items = new Dictionary<string, Action>();
            Dictionary<string, int> itemCount = new Dictionary<string, int>();
            Dictionary<string, ItemData> itemRefs = new Dictionary<string, ItemData>();

            foreach( var item in transform.GetComponent<Character>().items ) {
                itemRefs[item.title] = item;
                if( itemCount.ContainsKey(item.title) ) {
                    itemCount[item.title] += 1;
                } else {
                    itemCount.Add(item.title, 1);
                }
            }

            foreach( KeyValuePair<string, int> item in itemCount )
            {
                items.Add(item.Key + " (x" + item.Value + ")", () => {
                    if( WorldItems.lookup.ContainsKey(item.Key) )
                    {
                        InventoryItemData itemData = WorldItems.lookup[item.Key];
                        Character character = GameObject.Find("Player").GetComponent<Character>();
                        bool hpAllowed = character.multiplyHP(itemData.hp);

                        if( hpAllowed ) {
                            bool manaAllowed = character.multiplyMana(itemData.mana);
                            
                            if( manaAllowed ) {
                                string message = "Player used a";
                                if("aeiou".Contains(item.Key.ToLower()[0].ToString())) {
                                    message += "n";
                                }
                                message += " " + item.Key.ToLower() + ".";                                
                                if( itemData.message != "" )
                                {
                                    message += "\n" + itemData.message;
                                }

                                character.items.Remove(itemRefs[item.Key]);
                                character.SaveState();
                                GameObject.Find("ToastSystem").GetComponent<ToastSystem>().Open(message);
                                Destroy(GameObject.Find("Menu(Clone)"));
                            } else {
                                GameObject.Find("ToastSystem").GetComponent<ToastSystem>().Open("Not enough mana to use this item.");
                            }
                        } else {
                            GameObject.Find("ToastSystem").GetComponent<ToastSystem>().Open("Not enough HP to use this item.");
                        }
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
                    string message = "HP: " + player.currentHP + "/" + player.maxHP + "\nMana: " + player.currentMana + "/" + player.maxMana;
                    GameObject.Find("ToastSystem").GetComponent<ToastSystem>().Open(message);
                }},
                {"Items >>", delegate { menu.SubMenu(items); }},
                {"Map", () => {
                    GameObject.Find("ToastSystem").GetComponent<ToastSystem>().Open("Not implemented.");
                }},
                {"Save", () => {
                    transform.GetComponent<Character>().SaveState();
                    transform.GetComponent<PlayerMovement>().SaveState();
                    GameObject.Find("ToastSystem").GetComponent<ToastSystem>().Open("Saving...");
                }},
                {"Quit", () => SceneManager.LoadScene("Menu")},
                {"Return", () => {}},
            });
        } 
    }
}

