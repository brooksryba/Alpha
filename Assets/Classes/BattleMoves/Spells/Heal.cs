using UnityEngine;
public class Heal : Spell
{
    public string name = "Heal";

    public Heal(){
        moveName = "Spell";
        needsTarget = true;
        minigameName = "";
        defenseMinigameName = "";
    }

    public int GetHealthValue(){
        return 5;
    }

    public int GetManaCost(){
        return 5;
    }

    override public bool CheckFeasibility()
    {
        return true;
    }

    public override int GetMoveValueForAi()
    {
        if(IsUserAndTargetSameTeam())
            return 5;
        return -5;
    }

    override public void _ExecuteBattleMove() {
        Character caster = GetCharacter(userName);
        Character target = GetCharacter(targetName);
        target.Heal((int)(GetHealthValue()));
    }
}
