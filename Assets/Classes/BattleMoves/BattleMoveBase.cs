using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMoveBase
{
    public string userName;
    public string targetName;
    public bool needsTarget;
    public bool minigameSuccess;
    public double minigameMultiplier;
    public string minigameName = "";
    public string defenseMinigameName = "";
    public string animationName = "";
    public string moveType = "";
    public string moveName = "";
    public BattleObjectManager _manager = GameObject.Find("BattleObjectManager").GetComponent<BattleObjectManager>();
    
    virtual public void _ExecuteBattleMove() {
        // executes the effects of whatever move is selected, overriden in sub classes
        return;
    }

    virtual public bool CheckFeasibility(){
        // this function returns true if the player has enough mana, health, etc to execute attack
        return false;
    }

    virtual public int GetMoveValueForAi(){
        // for enemy ai, calculates the amount of value the move providers
        return -1;
    }

    public Character GetCharacter(string id)
    {
        return GameObject.Find(id).GetComponent<Character>();
    }

    public bool CanUseBattleMove(){
        return CheckInputs() & CheckFeasibility();
    }

    public bool CheckInputs(){
        if((targetName == "" || targetName == null) && needsTarget)
            return false;
        return true;
    }

    public bool IsEnemyUser(){
        return _manager.charManager.enemyParty.Contains(userName);    
    }

    public bool IsUserAndTargetSameTeam(){
        if(targetName=="")
            return false;
        bool bothEnemies = (IsEnemyUser() && _manager.charManager.enemyParty.Contains(targetName));
        bool bothFriendlies = (!IsEnemyUser() && _manager.charManager.playerParty.Contains(targetName));
        return (bothEnemies || bothFriendlies);   
    }

    public void ExecuteBattleMove() {
        if(CanUseBattleMove()){
            this._ExecuteBattleMove();
        }
        
            
    }


    static public BattleMoveBase GetBattleMoveClass(string moveName){
        switch(moveName){
            case "Basic Attack":
                return new BasicAttack();
            case "Heavy Attack":
                return new HeavyAttack();
            case "Very Heavy Attack":
                return new VeryHeavyAttack();
            case "Spread Attack":
                return new SpreadAttack();
            case "Heal":
                return new Heal();
            case "Boost Physical Attack":
                return new BoostPhysicalAttack();
            case "Poison Spell":
                return new PoisonSpell();
            case "Immobilize Spell":
                return new ImmobilizeSpell();
            default:
                return new BattleMoveBase();
        }
    }


}
