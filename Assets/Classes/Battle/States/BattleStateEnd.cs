public class BattleStateEnd : BattleState
{
    public void execute()
    {
        if(state == BattleState.WON)
        {
            dialogueText.text = "You won the battle!";
        } else if (state == BattleState.LOST)
        {
            dialogueText.text = "You were defeated";
        } else if (state == BattleState.RESIGN) {
            dialogueText.text = "You resigned the battle";
        }

        if(state != BattleState.RESIGN) {
            GameObject.FindWithTag("Player").GetComponent<Character>().SaveState();
            GameObject.Find("EnemyBattleStation").transform.GetChild(0).GetComponent<Character>().SaveState();

            foreach(GameObject go in GameObject.FindGameObjectsWithTag("Friendly")) {
                go.GetComponent<Character>().SaveState();
            }
        } else {
            GameObject.Find("EnemyBattleStation").transform.GetChild(0).GetComponent<Character>().dialogIndex = 0;
            GameObject.Find("EnemyBattleStation").transform.GetChild(0).GetComponent<Character>().SaveState();

            GameObject.FindWithTag("Player").GetComponent<Character>().currentHP = 0;
            GameObject.FindWithTag("Player").GetComponent<Character>().SaveState();

            RefreshAllHUDs();
        }

        Invoke("ReturnToWorld", 3);
    }
}
