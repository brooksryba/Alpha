using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BattleSystemMenu
{
    public BattleObjectManager _manager = BattleObjectManager.instance;

    public void OpenSubmenu(Character character, GameObject target)
    {
        closeOptionSubmenu();
        createOptionSubmenu(character);
        positionOptionSubmenu(target);
    }

    public void positionOptionSubmenu(GameObject target)
    {
        GameObject obj = GameObject.Find("MenuList");
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();
        obj.transform.position = cam.WorldToScreenPoint(target.transform.position);
        obj.transform.position += new Vector3(0, 150 * obj.transform.lossyScale.y, 0);
    }

    public void closeOptionSubmenu()
    {
        GameObject obj = GameObject.Find("Menu(Clone)");
        if(obj) 
        {
            GameObject.Destroy(obj);
        }
    }



    public void createOptionSubmenu(Character character)
    {
        GameObject obj = GameObject.Instantiate(Resources.Load("Prefabs/Widgets/Menu")) as GameObject;
        DynamicMenu menu = obj.GetComponent<DynamicMenu>();

        Dictionary<string, Action> attacks = new Dictionary<string, Action>();
        Dictionary<string, Action> spells = new Dictionary<string, Action>();
        Dictionary<string, Action> strategies = new Dictionary<string, Action>();

        // Localization.get(attack.title)
        // int > pass to selection callback
        foreach( Move attackMove in character.condition.attacks ) {
            string attackName = LocalizationData.data[attackMove.moveID];
            attacks.Add("> "+attackName, () => { _manager.chosenMove = attackMove; });
        }
        attacks.Add("Return", () => { });

        foreach( Move spellMove in character.condition.spells ) {
            string spellName = LocalizationData.data[spellMove.moveID];
            spells.Add("> "+spellName, () => { _manager.chosenMove = spellMove; });
        }
        spells.Add("Return", () => { });
        
        Dictionary<string, Action> items = new Dictionary<string, Action>();
        if(character.condition.items.Count > 0){
            foreach( (Item item, int itemQuantity) in character.condition.items ) { 
                string itemName = LocalizationData.data[item.itemID];
                items.Add("> "+itemName + " (x" + itemQuantity.ToString() + ")", () => { _manager.chosenItem = item; });
            }



        }


        items.Add("Return", () => { });

        strategies.Add("Return", () => { });

        menu.OpenWithTag(new Dictionary<string, Action>(){
        {">> Attacks", delegate { menu.SubMenu(attacks); }},
        {">> Spells", delegate { menu.SubMenu(spells); }},
        {">> Items", delegate { menu.SubMenu(items); }},
        {">> Strategies", delegate { menu.SubMenu(strategies); }},
        {"> Resign", delegate { _manager.playerResigned = true; }},
        {"Return", () => {}},
        });
    
    
    }    

}