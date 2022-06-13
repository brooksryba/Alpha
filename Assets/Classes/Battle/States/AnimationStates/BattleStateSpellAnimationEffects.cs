using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateSpellAnimationEffects : BattleState
{

    public BattleSpriteController spriteController;

    override public IEnumerator execute()
    {
        newState = this;
        if(_manager.spellObj){
            GameObject.Destroy(_manager.spellObj);
            spriteController = _manager.charManager.defender.GetComponent<BattleSpriteController>();
            
        }

        if (_manager.charManager.defender) {
            if(spriteController) {
                spriteController.TransitionColors(spriteController.sprite.color, new Color (1f, 0f, 0f, 0.75f), 2.0f);
                yield return new WaitForSeconds(1.5f);
        
                // @todo - The colors should be different based on spell type (if this is how spells move forward)
            }
        }

        _manager.battleSystemHud.RefreshAllHUDs();


        // animate players who have died
        for(int i = 0; i < _manager.charManager.allPlayers.Count; i++){
            if(battleSystemUtils.CheckPlayerDeadAndAnimate(_manager.charManager.allPlayers[i]))
                _manager.charManager.deadPlayerList.Add(_manager.charManager.allPlayers[i]);
        }        

        newState = new BattleStateSpellAnimationRetreat();

    }
}
