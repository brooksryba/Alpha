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

    public Text manaText;
    public Slider manaSlider;
    public Button playerButton;

    public Character character;

    void Start()
    {
        playerButton.onClick.AddListener(toggleSubmenu);
    }

    public void Refresh()
    {
        nameText.text = character.title;
        levelText.text = "Lvl " + character.level;
        hpSlider.maxValue = character.maxHP;
        hpSlider.value = character.currentHP;
        hpText.text = character.currentHP.ToString() + " HP";

        manaSlider.maxValue = character.maxMana;
        manaSlider.value = character.currentMana;
        manaText.text = character.currentMana.ToString() + " Mana";
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
        Character self = GameObject.Find("Player(Clone)").GetComponent<Character>();
        Character enemy = GameObject.Find("Enemy0(Clone)").GetComponent<Character>();

        attacks.Add("Basic Attack", () => { bSystem.OnAttackButton(Attacks.basicAttack, self, enemy); });
        attacks.Add("Heavy Attack", () => { bSystem.OnAttackButton(Attacks.heavyAttack, self, enemy); });
        attacks.Add("Return", () => { });


        spells.Add("Heal", () => { bSystem.OnHealButton(); });
        spells.Add("Fire Damage", () => { });
        spells.Add("Return", () => { });

        strategies.Add("Charge Mana", () => { });

        Dictionary<string, Action> items = new Dictionary<string, Action>();
        foreach (var item in GameObject.Find("PlayerBattleStation/Player(Clone)").GetComponent<Player>().items)
        {
            items.Add(item.title, () => { });
        }
        items.Add("Return", () => { });


        menu.Open(new Dictionary<string, Action>(){
        {"Attacks", delegate { menu.SubMenu(attacks); }},
        {"Spells", delegate { menu.SubMenu(spells); }},
        {"Items", delegate { menu.SubMenu(items); }},
        {"Strategies", delegate { menu.SubMenu(strategies); }},
        {"Return", () => {}},
    });
    }
}
