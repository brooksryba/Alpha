using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleHUD : MonoBehaviour
{
    public GameObject nameText;
    public GameObject levelText;
    public GameObject hpText;
    public Slider hpSlider;

    public GameObject manaText;
    public Slider manaSlider;
    public Button playerButton;

    public Character character;

    void Start()
    {
        playerButton.onClick.AddListener(OnClick);
    }

    public void OnClick()
    {
        BattleSystemController.instance.BattleHudTitleButton(character.title, gameObject);
    }

    public void Refresh()
    {
        nameText.GetComponent<TMP_Text>().SetText(character.title);
        levelText.GetComponent<TMP_Text>().SetText("Lv" + character.level);

        hpSlider.maxValue = character.characterClass.maxHP;
        hpSlider.value = character.currentHP;
        hpText.GetComponent<TMP_Text>().SetText(character.currentHP.ToString() + " HP");

        manaSlider.maxValue = character.characterClass.maxMana;
        manaSlider.value = character.currentMana;
        manaText.GetComponent<TMP_Text>().SetText(character.currentMana.ToString() + " Mana");

        playerButton.interactable = character.currentHP > 0;
    }

}
