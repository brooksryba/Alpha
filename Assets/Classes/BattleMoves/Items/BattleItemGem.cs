using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleItemGem : BattleItem
{
    public BattleItemGem(){
        moveName = "Gem";
        needsTarget = false;
        minigameName = "";
        defenseMinigameName = "";
    }

    override public bool CheckFeasibility()
    {
        return true;
    }

    override public int GetMoveValueForAi()
    {
        return -1;
    }

    override public void _ExecuteBattleMove() {
        Character attacker = GetCharacter(userName);
        attacker.multiplyMana(GetWorldItemData().mana);
        attacker.multiplyHP(GetWorldItemData().hp);
    }
}
