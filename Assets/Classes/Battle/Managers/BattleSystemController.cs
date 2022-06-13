using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystemController : MonoBehaviour
{
    public static BattleSystemController instance;

    public void Awake()
    {
        instance = this;
    }

    public event Action<string, GameObject> onBattleHudTitleButton;
    public void BattleHudTitleButton(string character, GameObject target)
    {
        if(onBattleHudTitleButton != null)
            onBattleHudTitleButton(character, target);
    }
}
