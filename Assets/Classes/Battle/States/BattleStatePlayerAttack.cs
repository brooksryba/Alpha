public class BattleStatePlayerAttack : BattleState
{
    public void execute()
    {
        bool isAccepted = DoAttack(attackReference, ref playerUnit, ref enemyUnit);
        bool isDead = enemyUnit.currentHP <= 0;

        RefreshAllHUDs();
        if(isAccepted){
            
            dialogueText.text = "The attack is successful";
            RefreshAllHUDs();
            yield return new WaitForSeconds(2f);
            if(isDead){
                bool allDead = PartyDead(enemyParty);
                if(allDead){
                    state = BattleState.WON;
                    EndBattle();
                }
            }
            GetNextAttacker();
        }
        else {
            dialogueText.text = playerUnit.title + " cannot choose this attack";
            yield return new WaitForSeconds(2f);
            PlayerTurnStart();
        }
                
        return this;
    }
}
