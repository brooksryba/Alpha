using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStateSpellAnimationRetreat : BattleState
{

    override public IEnumerator execute()
    {
        newState = this;
        BattleSpriteController spriteController = _manager.defender.GetComponent<BattleSpriteController>();


        if (_manager.defender) {
            if(spriteController) {
                spriteController.TransitionColors(spriteController.sprite.color, _manager.originalSpriteColors[_manager.defenderName], 2.0f);
                yield return new WaitForSeconds(1f);
        
            }
        } 


        newState = new BattleStateAttackEnd();
        yield return newState;
        
    }
}
