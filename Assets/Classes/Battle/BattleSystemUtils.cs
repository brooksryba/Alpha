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


    // @todo - repeating code for these, should chosenAttack be a public var?
    public bool DoAttack(string attackName, Character attacker, Character defender){
        Attack chosenAttack = attackLibrary.GetAttackClass(attackName);
        chosenAttack.attackerName = attacker.title;
        if(defender == null){
            chosenAttack.defenderName = "";
        } else {
            chosenAttack.defenderName = defender.title;
        }
        return chosenAttack.DoAttack();
    }

    public bool ConfirmAttackInputs(string attackName, Character attacker, Character defender){
        Attack chosenAttack = attackLibrary.GetAttackClass(attackName);
        chosenAttack.attackerName = attacker.title;
        if(defender == null){
            chosenAttack.defenderName = "";
        } else {
            chosenAttack.defenderName = defender.title;
        }
        return chosenAttack.CheckAttackInputs();
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
