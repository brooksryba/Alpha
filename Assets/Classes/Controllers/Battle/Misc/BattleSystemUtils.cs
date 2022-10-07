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
        // BattleMoveBase chosenMove = BattleMoveBase.GetBattleMoveClass(moveName);
        // if(!isEnemy)
        //     return chosenMove.minigameName;
        // return chosenMove.defenseMinigameName;
        return "";
    }

    public bool CheckPlayerDeadAndAnimate(int characterID){
        GameObject playerObj = PrefabManager.Get(characterID, PrefabManager.Types.Character);
        Character player = playerObj.GetComponent<Character>();
        (int currentHP, int maxHP) = player.condition.hp;
        if(currentHP == 0){
            BattleSpriteController spriteController = playerObj.GetComponent<BattleSpriteController>();
            spriteController.TransitionColors(spriteController.sprite.color, new Color (0.25f, 0.25f, 0.25f, 0.25f), 3.0f);
            return true;
        }
        return false;
    }


    // public BattleMoveBase PrepChosenBattleMove(string moveName, Character user, Character target){
    //     BattleMoveBase chosenMove = BattleMoveBase.GetBattleMoveClass(moveName);
    //     chosenMove.userName = user.title;
    //     if(target == null){
    //         chosenMove.targetName = "";
    //     } else {
    //         chosenMove.targetName = target.title;
    //     }
    //     return chosenMove;
    // }


    public void ExecuteBattleMove(string moveName, Character user, Character target, double minigameMultiplier=1.0, bool minigameSuccess=false){
        // chosenMove.minigameMultiplier = minigameMultiplier;
        // chosenMove.minigameSuccess = minigameSuccess;
        // chosenMove.ExecuteBattleMove();

        // this will apply the logic from the attributes of the select move and target
    }

    public bool ConfirmBattleMoveInputs(string moveName, Character user, Character target){
        // BattleMoveBase chosenMove = PrepChosenBattleMove(moveName, user, target);
        // check if move type and target are selected if needed
        return true;
    }

    public bool ConfirmBattleMoveFeasibility(string moveName, Character user, Character target){
        // BattleMoveBase chosenMove = PrepChosenBattleMove(moveName, user, target);
        // check if user has enough mana / hp to execute the move
        return true;
    }



    public bool PartyDead(List<int> partyMembers)
    {
        foreach(int id in partyMembers)
        {
            Character member = CharacterManager.Get(id);
            (int currentHP, int maxHP) = member.condition.hp;
            if(currentHP > 0)
                return false;
        }
        return true;
    }




    // @TODO - All below is from the old battle bonus manager script, will need to be refactored

    public void AddBonus(string playerName, string statName, double statMultiplier, double statAddition, int bonusDuration){
        // BattleBonus newBonus = new BattleBonus(playerName, statName, statMultiplier, statAddition, bonusDuration);
        // battleBonuses.Add(newBonus);
    }



    public void DestroyAllBonuses(){
        // battleBonuses = new List<BattleBonus>();
    }


    public int GetBattleStat(string playerName, string statName, int initialValue, bool applyBonus=true)
    {
        // if(!applyBonus)
        //     return initialValue;
        // double multiplier = 1.0;
        // double addition = 0.0;
        // for(int i = 0; i < battleBonuses.Count; i++){
        //     BattleBonus checkBonus = battleBonuses[i];
        //     if(checkBonus.playerName==playerName && checkBonus.statName == statName && checkBonus.bonusDuration > 0){
        //         multiplier = multiplier*battleBonuses[i].statMultiplier;
        //         addition += battleBonuses[i].statAddition;
        //     }
        // }
        // return (int)(initialValue*multiplier + addition);
        return 1;
    }

    public bool CheckSkipTurn(string playerName){
        // @todo - no way to stack skip turns, but it might not be something we would always want anyway
        // for(int i = 0; i < battleBonuses.Count; i++){
        //     BattleBonus checkBonus = battleBonuses[i];
        //     if(checkBonus.playerName==playerName && checkBonus.statName == "skipTurn" && checkBonus.bonusDuration > 0){
        //         return true;
        //     }
        // }
        return false;
    }



    //     public void BattleBonusAction(){
    //     // @todo - this should probably be handled elsewhere, but could not find ideal location
    //     if(statName == "currentHp"){
    //         Character currentPlayer = GameObject.Find(playerName).GetComponent<Character>();
    //         currentPlayer.multiplyHP(statMultiplier);
    //         if(statAddition > 0){
    //             currentPlayer.Heal((int)statAddition);
    //         } else {
    //             currentPlayer.TakeDamage((int)(-1.0*statAddition));
    //         }
            
    //     }

    //     if(statName == "currentMana"){
    //         Character currentPlayer = GameObject.Find(playerName).GetComponent<Character>();
    //         currentPlayer.multiplyHP(statMultiplier);
    //         if(statAddition > 0){
    //             currentPlayer.UseMana((int)statAddition);
    //         } else {
    //             currentPlayer.AddMana((int)statAddition);
    //         }
            
    //     }


    // }

}
