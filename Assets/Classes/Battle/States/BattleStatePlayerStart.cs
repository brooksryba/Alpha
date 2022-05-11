public class BattleStatePlayerStart : BattleState
{
    public void execute()
    {
        dialogueText.text = "Choose a target for "+playerUnit.title+":";
        state = BattleState.PLAYERTURN_AWAIT_TARGET;
                
        return this;
    }
}
