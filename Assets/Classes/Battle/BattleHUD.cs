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
        playerButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        BattleSystemController.instance.BattleHudTitleButton(character.title);
    }

    public void Refresh()
    {
        nameText.text = character.title;
        levelText.text = "Lv" + character.level;
        hpSlider.maxValue = character.maxHP;
        hpSlider.value = character.currentHP;
        hpText.text = character.currentHP.ToString() + " HP";

        manaSlider.maxValue = character.maxMana;
        manaSlider.value = character.currentMana;
        manaText.text = character.currentMana.ToString() + " Mana";

        playerButton.interactable = character.currentHP > 0;
    }

}
