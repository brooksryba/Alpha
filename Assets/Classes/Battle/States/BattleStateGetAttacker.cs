public class BattleStateGetAttacker : BattleState
{
    public void execute()
    {
        // Loops through ordered list of all characters by speed and selects the next in order to attack
        // Potential concerns - Are we allowing speed changes mid-battle, if so how do we handle that? Currently, the order of the list will change, but turn number does not reset

        turnIndex += 1;
        int totalPlayers = playerParty.Count + enemyParty.Count;
        if(turnIndex >= totalPlayers)
            turnIndex = 0;
        List<Character> allCharacters = new List<Character>();
        foreach(var p in playerParty){
            allCharacters.Add(GetCharacter(p));
        }
        foreach(var p in enemyParty){
            allCharacters.Add(GetCharacter(p));
        }

        allCharacters.Sort(delegate(Character a, Character b){
            return (b.speed).CompareTo(a.speed); // highest speed first (a comp to b is lowest)
        });

        for(int i = 0; i < allCharacters.Count; i++){
            int indexToCheck = (turnIndex + i) % allCharacters.Count;
            if(allCharacters[indexToCheck].currentHP > 0){
                turnIndex = indexToCheck;
                break;
            }
        }
        
        Character nextUp = allCharacters[turnIndex];
        if(playerParty.Contains(nextUp.title)){
            dialogueText.text = "It is "+nextUp.title+"'s turn to attack!";
            playerUnit = nextUp;
            disableUnusableHuds(nextUp.title);
            return new BattleStatePlayerStart();
        } else {
            dialogueText.text = "It is "+nextUp.title+"'s turn to attack!";
            enemyUnit = nextUp;
            return new BattleStateEnemyStart();
        }

        return this;
    }
}
