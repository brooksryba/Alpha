using UnityEngine;
using System.Collections.Generic;
public class BattleSystemUtils
{

    public Character GetCharacter(string id)
    {
        if(id!="" && id!=null)
            return GameObject.Find(id).GetComponent<Character>();
        return null;
    }

    public string GetMinigameNameFromBattleMove(string moveName, bool isEnemy){
        BattleMoveBase chosenMove = BattleMoveBase.GetBattleMoveClass(moveName);
        if(!isEnemy)
            return chosenMove.minigameName;
        return chosenMove.defenseMinigameName;
    }

    public bool CheckPlayerDeadAndAnimate(string id){
        GameObject playerObj = GameObject.Find(id);
        Character player = playerObj.GetComponent<Character>();
        if(player.currentHP == 0){
            BattleSpriteController spriteController = playerObj.GetComponent<BattleSpriteController>();
            spriteController.TransitionColors(spriteController.sprite.color, new Color (0.25f, 0.25f, 0.25f, 0.25f), 3.0f);
            return true;
        }
        return false;
    }


    public BattleMoveBase PrepChosenBattleMove(string moveName, Character user, Character target){
        BattleMoveBase chosenMove = BattleMoveBase.GetBattleMoveClass(moveName);
        chosenMove.userName = user.title;
        if(target == null){
            chosenMove.targetName = "";
        } else {
            chosenMove.targetName = target.title;
        }
        return chosenMove;
    }


    public void ExecuteBattleMove(string moveName, Character user, Character target, double minigameMultiplier=1.0, bool minigameSuccess=false){
        BattleMoveBase chosenMove = PrepChosenBattleMove(moveName, user, target);
        chosenMove.minigameMultiplier = minigameMultiplier;
        chosenMove.minigameSuccess = minigameSuccess;
        chosenMove.ExecuteBattleMove();
    }

    public bool ConfirmBattleMoveInputs(string moveName, Character user, Character target){
        BattleMoveBase chosenMove = PrepChosenBattleMove(moveName, user, target);
        return chosenMove.CheckInputs();
    }

    public bool ConfirmBattleMoveFeasibility(string moveName, Character user, Character target){
        BattleMoveBase chosenMove = PrepChosenBattleMove(moveName, user, target);
        return chosenMove.CheckFeasibility();
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
