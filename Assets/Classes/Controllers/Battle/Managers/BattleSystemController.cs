using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystemController : MonoBehaviour
{    
    private static BattleSystemController _instance;
    public static BattleSystemController instance { get { return _instance; } }

    private void Awake() { _instance = this; }
    public event Action<string, GameObject> onBattleHudTitleButton;
    public void BattleHudTitleButton(string character, GameObject target)
    {
        if(onBattleHudTitleButton != null)
            onBattleHudTitleButton(character, target);
    }
}
