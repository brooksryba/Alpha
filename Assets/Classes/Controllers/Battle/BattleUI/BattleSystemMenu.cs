using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BattleSystemMenu
{
    public BattleObjectManager _manager = GameObject.Find("BattleObjectManager").GetComponent<BattleObjectManager>();

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
        obj.transform.position += new Vector3(0, 100, 0);
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

        foreach( var attackName in character.attackNames ) {
            attacks.Add(attackName, () => { _manager.chosenBattleMove = attackName; });
        }
        attacks.Add("Return", () => { });

        foreach( var spellName in character.spellNames ) {
            spells.Add(spellName, () => { _manager.chosenBattleMove = spellName; });
        }
        spells.Add("Return", () => { });
        
        Dictionary<string, Action> items = new Dictionary<string, Action>();
        Dictionary<string, int> itemCount = new Dictionary<string, int>();
        Dictionary<string, ItemData> itemRefs = new Dictionary<string, ItemData>();

        if(character.items.Count > 0){
            foreach( var item in character.items ) {               
            itemRefs[item.title] = item;
            if( itemCount.ContainsKey(item.title) ) {
                itemCount[item.title] += 1;
            } else {
                itemCount.Add(item.title, 1);
            }
            }

            foreach(KeyValuePair<string, int> item in itemCount ) {
                items.Add(item.Key + "(x" + item.Value.ToString() + ")", () => { _manager.chosenItem = item.Key; });
            }

        }


        items.Add("Return", () => { });

        strategies.Add("Return", () => { });

        menu.Open(new Dictionary<string, Action>(){
        {"Attacks >>", delegate { menu.SubMenu(attacks); }},
        {"Spells >>", delegate { menu.SubMenu(spells); }},
        {"Items >>", delegate { menu.SubMenu(items); }},
        {"Strategies >>", delegate { menu.SubMenu(strategies); }},
        {"Resign", delegate { _manager.battleStateMachine.Transition(new BattleStateResign()); }},
        {"Return", () => {}},
        });
    
    
    }    

}