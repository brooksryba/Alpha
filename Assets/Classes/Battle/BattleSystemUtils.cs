using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class BattleSystemUtils
{
    public Attack attackLibrary = new Attack();

    public Character GetCharacter(string id)
    {
        return GameObject.Find(id).GetComponent<Character>();
    }


    public bool DoAttack(string attackName, Character attacker, Character defender){
        Attack chosenAttack = attackLibrary.getAttackClass(attackName);
        return chosenAttack.doAttack(attacker.title, defender.title);
    }


    public bool PartyDead(List<string> partyMembers)
    {
        foreach(var id in partyMembers)
        {
            Character member = GetCharacter(id);
            if(member.currentHP > 0)
                return false;
        }
        return true;
    }

}
