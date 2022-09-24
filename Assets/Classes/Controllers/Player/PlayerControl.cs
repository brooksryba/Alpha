using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{
    public void OnReset() {
        SaveSystem.instance.ResetAndDeregister();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
    }

    public void OnSave() {
        SaveSystem.instance.Save();
        ToastSystem.instance.Open("Saving...");
    }

    public void OnInteract() {
        GameObject.Find("Player").GetComponent<PlayerInteraction>().HandleCollisionInteraction();
    }

    public void OnOptions() {
        if( GameObject.Find("Menu(Clone)") ) { return; }
        
        GameObject obj = Instantiate(Resources.Load("Prefabs/Widgets/Menu"), transform.position, transform.rotation) as GameObject;
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

                    ToastSystem.instance.Open(message);
                    Destroy(GameObject.Find("Menu(Clone)"));
                } 
                else
                {
                    ToastSystem.instance.Open("Can't use this item now.");
                }
            });
        }
        items.Add("Return", () => {});

        Character player = GameObject.Find("Player").GetComponent<Character>();
        Dictionary<string, Action> playerSub = new Dictionary<string, Action>();
        playerSub.Add("Level: " + player.level, () => {});
        playerSub.Add("HP: " + player.currentHP + "/" + player.characterClass.maxHP, () => {});
        playerSub.Add("Mana: " + player.currentMana + "/" + player.characterClass.maxMana, () => {});
        List<int> xpInterval = LevelSystem.instance.GetXpInterval(player.earnedXp);
        playerSub.Add("XP: " + xpInterval[0].ToString() + "/" + xpInterval[1].ToString(), () => {});
        playerSub.Add("Return", () => {});

        menu.Open(new Dictionary<string, Action>(){
            {">> Player", delegate { menu.SubMenu(playerSub); }},
            {">> Items", delegate { menu.SubMenu(items); }},
            {"> Map", () => {
                ToastSystem.instance.Open("Not implemented.");
            }},
            {"> Save", () => {
                SaveSystem.instance.Save();
                ToastSystem.instance.Open("Saving...");
            }},
            {"> Quit", () => SceneManager.LoadScene("Menu")},
            {"Return", () => {}},
        });
    }
}

