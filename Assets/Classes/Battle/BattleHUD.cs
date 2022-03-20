using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{
   public Text nameText;
   public Text levelText;
   public Text hpText;
   public Slider hpSlider;

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
}
