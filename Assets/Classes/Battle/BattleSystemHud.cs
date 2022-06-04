using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BattleSystemHud
{
    public BattleSystemUtils utils = new BattleSystemUtils();

    public bool canSelect = false;
    public Character selection; // this is who the user picks as the target of an attack, spell, etc
    public GameObject selectionButton;
    public void createSingleHUD(ref GameObject partyMember, ref Character hudCharacter, GameObject partyContainer)
    {
        GameObject battleHudPrefab = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/BattleHUD"), partyContainer.transform);
        battleHudPrefab.transform.SetParent(partyContainer.transform);
        BattleHUD hud = battleHudPrefab.GetComponent<BattleHUD>();
        hud.character = hudCharacter;
        hud.Refresh();        
    }

    public void RefreshAllHUDs()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("BattleHUD");
        foreach(var hud in objs){
            BattleHUD battleHud = hud.GetComponent<BattleHUD>();
            Character updateHudCharacter = utils.GetCharacter(battleHud.character.title);
            battleHud.character = updateHudCharacter;
            battleHud.Refresh();
        }
    }

    public void disableUnusableHuds(string usableHudName, List<string> playerParty){
        GameObject[] objs = GameObject.FindGameObjectsWithTag("BattleHUD");
        foreach(var hud in objs){
            BattleHUD battleHud = hud.GetComponent<BattleHUD>();
            if (playerParty.Contains(battleHud.character.title)){
            if (battleHud.character.title==usableHudName)
                continue;
            else 
                battleHud.playerButton.interactable = false;
            }
        }
    }

    public void OnHUDTitleButton(string characterID, GameObject target)
    {
        if( canSelect == true ) {
            selection = utils.GetCharacter(characterID);
            selectionButton = target;
        }
    }  

      
}