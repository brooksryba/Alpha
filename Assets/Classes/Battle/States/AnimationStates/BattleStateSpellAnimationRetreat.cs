using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStateSpellAnimationRetreat : BattleState
{

    override public IEnumerator execute()
    {
        newState = this;
        BattleSpriteController spriteController = _manager.charManager.defender.GetComponent<BattleSpriteController>();


        if (_manager.charManager.defender) {
            if(spriteController) {
                spriteController.TransitionColors(spriteController.sprite.color, _manager.charManager.originalSpriteColors[_manager.charManager.defenderName], 2.0f);
                yield return new WaitForSeconds(1f);
        
            }
        } 


        newState = new BattleStateAttackEnd();
        yield return newState;
        
    }
}
