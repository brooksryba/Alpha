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
        levelText.GetComponent<TMP_Text>().SetText("Lv" + character.condition.level);

        (int currentHP, int maxHP) = character.condition.hp;
        hpSlider.maxValue = (float)maxHP;
        hpSlider.value = (float)currentHP;
        hpText.GetComponent<TMP_Text>().SetText(currentHP.ToString() + " HP");

        (int currentMana, int maxMana) = character.condition.hp;
        manaSlider.maxValue = (float)maxMana;
        manaSlider.value = (float)currentMana;
        manaText.GetComponent<TMP_Text>().SetText(currentMana.ToString() + " Mana");

        playerButton.interactable = currentHP > 0;
    }

}
