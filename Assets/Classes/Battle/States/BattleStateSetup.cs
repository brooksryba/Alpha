public class BattleStateSetup : BattleState
{
    public void execute()
    {
        BattleSystemController.instance.onBattleHudTitleButton += OnHUDTitleButton;

        GameObject playerPrefab = Resources.Load("Prefabs/Characters/" + battleScriptable.enemy) as GameObject;
        GameObject enemyPrefab = null;
        if(battleScriptable.enemy != null && battleScriptable.enemy != ""){           
            enemyPrefab = Resources.Load("Prefabs/Characters/" + battleScriptable.enemy) as GameObject;
        }

        GameObject playerGO = initializeParty(ref playerParty, ref playerPrefab, playerBattleStation, playerPartyContainer);
        playerUnit = playerGO.GetComponent<Character>();

        GameObject enemyGO = initializeParty(ref enemyParty, ref enemyPrefab, enemyBattleStation, enemyPartyContainer, true);
        enemyUnit = enemyGO.GetComponent<Character>();

        dialogueText.text = enemyUnit.title + " engages in battle...";

        yield return new WaitForSeconds(1f);

        return new BattleStateGetAttacker();
    }

    public GameObject initializeParty(ref List<string> partyList, ref GameObject partyLeaderPrefab, GameObject battleStationContainer, GameObject partyContainer, bool flip=false)
    {
        GameObject partyLeaderObj = Instantiate(partyLeaderPrefab, battleStationContainer.transform);
        partyLeaderObj.transform.SetParent(battleStationContainer.transform);
        if(flip)
            partyLeaderObj.GetComponent<SpriteRenderer>().flipX = true;
        Character partyLeader = partyLeaderPrefab.GetComponent<Character>();
        partyLeaderObj.name = partyLeader.title;
        partyLeader.LoadState();
        partyList.Add(partyLeader.title);

        createSingleHUD(ref partyLeaderObj, ref partyLeader, partyContainer);

        int index = 0;
        foreach(var pm in partyLeader.partyMembers){
            index += 1;
            GameObject partyMemberObject = Instantiate(Resources.Load<GameObject>("Prefabs/" + pm), battleStationContainer.transform);
            Character partyMemberChar = partyMemberObject.GetComponent<Character>();
            partyMemberObject.name = partyMemberChar.title;
            partyMemberChar.LoadState();
            partyList.Add(partyMemberChar.title);

            // @todo - right now it puts next party member down 2 * its height. Should try and make this more flexible
            partyMemberObject.transform.SetParent(battleStationContainer.transform);
            partyMemberObject.transform.position = partyMemberObject.transform.position - new Vector3(0.0f, 2 * index, 0.0f);
            if(flip)
                partyMemberObject.GetComponent<SpriteRenderer>().flipX = true;


            createSingleHUD(ref partyMemberObject, ref partyMemberChar, partyContainer);
        }
        return partyLeaderObj;
    }
        
}
