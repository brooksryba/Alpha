using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleHUD : MonoBehaviour
{
   public Text nameText;
   public Text levelText;
   public Text hpText;
   public Slider hpSlider;
    public Button playerButton;

   void Start()
   {
       playerButton.onClick.AddListener(toggleSubmenu);
   }

   public void SetHUD(Unit unit)
   {
       nameText.text = unit.unitName;
       levelText.text = "Lvl " + unit.unitLevel;
       hpSlider.maxValue = unit.maxHP;
       hpSlider.value = unit.currentHP;
       hpText.text = unit.currentHP.ToString() + " HP";
   }

   public void SetHP(int hp)
   {
       hpSlider.value = hp;
       hpText.text = hp.ToString() + " HP";
   }

   public void toggleSubmenu()
   {
    GameObject obj = Instantiate(Resources.Load("Prefabs/Menu"), transform.position, transform.rotation) as GameObject;
    DynamicMenu menu = obj.GetComponent<DynamicMenu>();

    Dictionary<string, Action> attacks = new Dictionary<string, Action>();
    Dictionary<string, Action> spells = new Dictionary<string, Action>();
    Dictionary<string, Action> strategies = new Dictionary<string, Action>();

    // place to loop through attacks, spells, etc and add
    // the battle system should not hold attack and heal functions
    BattleSystem bSystem = GameObject.Find("BattleSystem").GetComponent<BattleSystem>();

    attacks.Add("Basic Attack", () => {bSystem.OnAttackButton();});
    attacks.Add("Heavy Attack", () => {});
    attacks.Add("Return", () => {});
    

    spells.Add("Heal", () => {bSystem.OnHealButton();});
    spells.Add("Fire Damage", () => {});
    spells.Add("Return", () => {});

    strategies.Add("Charge Mana", () => {});

    Dictionary<string, Action> items = new Dictionary<string, Action>();
    foreach( var item in GameObject.Find("PlayerBattleStation/Player(Clone)").GetComponent<Player>().items )
    {
        items.Add(item.title, () => {});
    }
    items.Add("Return", () => {});


    menu.Open(new Dictionary<string, Action>(){
        {"Attacks", delegate { menu.SubMenu(attacks); }},
        {"Spells", delegate { menu.SubMenu(spells); }},
        {"Items", delegate { menu.SubMenu(items); }},
        {"Strategies", delegate { menu.SubMenu(strategies); }},
        {"Return", () => {}},
    });
   }
}
