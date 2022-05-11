public class BattleStateEnemyAttack : BattleState
{
    public void execute()
    {
        state = BattleState.ENEMYTURN_AWAIT_MOVE; // state changes will be needed when AI is implemented, but not useful yet
        state = BattleState.ENEMYTURN_AWAIT_TARGET;
        dialogueText.text = enemyUnit.title + " attacks " + playerUnit.title + "!";

        yield return new WaitForSeconds(1f);
        // @todo - this is where the enemies AI should be implemented

        state = BattleState.ENEMYTURN_ATTACKING;
        bool isDead = playerUnit.TakeDamage(5); // enemyParty[currentEnemy].damage);

        RefreshAllHUDs();

        yield return new WaitForSeconds(1f);

        if(isDead){
            bool allDead = PartyDead(playerParty);
            if(allDead){
                state = BattleState.LOST;
                EndBattle();
            }
        }

        GetNextAttacker();
                
        return this;
    }
}
