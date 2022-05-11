public class BattleSystemHud
{
    public void createSingleHUD(ref GameObject partyMember, ref Character hudCharacter, GameObject partyContainer)
    {
        GameObject battleHudPrefab = Instantiate(Resources.Load<GameObject>("Prefabs/BattleHUD"), partyContainer.transform);
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
            Character updateHudCharacter = GetCharacter(battleHud.character.title);
            battleHud.character = updateHudCharacter;
            battleHud.Refresh();
        }
    }

    public void disableUnusableHuds(string usableHudName){
        // this should probably be done in a HUD manager or equivalent 
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
        Character character = GetCharacter(characterID);
        if(state == BattleState.PLAYERTURN_AWAIT_MOVE)
        {
            playerUnit = character;
            OpenSubmenu(character, target);
        }   
        else if(state == BattleState.PLAYERTURN_AWAIT_TARGET)
        {
            enemyUnit = character;
            state = BattleState.PLAYERTURN_ATTACKING;
            PlayerTurnAttack();
        }
    }        
}