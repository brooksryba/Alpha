using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class BattleSystemUtils
{
    public Attack attackLibrary = new Attack();

    public Character GetCharacter(string id)
    {
        return GameObject.Find(id).GetComponent<Character>();
    }

    public string GetMinigameNameFromAttack(string attackName, bool isEnemy){
        Attack chosenAttack = attackLibrary.GetAttackClass(attackName);
        if(!isEnemy)
            return chosenAttack.minigameName;
        return chosenAttack.defenseMinigameName;
    }

    public bool CheckPlayerDeadAndAnimate(string id){
        // check dead players and handles death animation
        GameObject playerObj = GameObject.Find(id);
        Character player = playerObj.GetComponent<Character>();
        if(player.currentHP == 0){
            BattleSpriteController spriteController = playerObj.GetComponent<BattleSpriteController>();
            spriteController.TransitionColors(spriteController.sprite.color, new Color (0.25f, 0.25f, 0.25f, 0.25f), 3.0f);
            return true;
        }
        return false;
    }

    public Attack PrepChosenAttack(string attackName, Character attacker, Character defender){
        Attack chosenAttack = attackLibrary.GetAttackClass(attackName);
        chosenAttack.attackerName = attacker.title;
        if(defender == null){
            chosenAttack.defenderName = "";
        } else {
            chosenAttack.defenderName = defender.title;
        }
        return chosenAttack;
    }


    public void DoAttack(string attackName, Character attacker, Character defender, double damageMultiplier=1.0){
        Attack chosenAttack = PrepChosenAttack(attackName, attacker, defender);
        chosenAttack.damageMultiplier = damageMultiplier;
        chosenAttack.DoAttack();
    }

    public bool ConfirmAttackInputs(string attackName, Character attacker, Character defender){
        Attack chosenAttack = PrepChosenAttack(attackName, attacker, defender);
        return chosenAttack.CheckAttackInputs();
    }

    public bool ConfirmCanUseAttack(string attackName, Character attacker, Character defender){
        Attack chosenAttack = PrepChosenAttack(attackName, attacker, defender);
        return chosenAttack.CanUseAttack();
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
