using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateUseItem : BattleState
{
    public override IEnumerator enter()
    {
        _manager.battleSystemMenu.closeOptionSubmenu();
        _manager.battleSystemHud.canSelect = true;       
        return base.enter();
    }

    override public IEnumerator execute()
    {
        if(BattleItems.lookup.ContainsKey(_manager.chosenItem)) {
            InventoryItemData itemData = BattleItems.lookup[_manager.chosenItem];
            Character activeCharacter = _manager.charManager.attacker.GetComponent<Character>();
            Toast(itemData.Execute(activeCharacter));
            Transition(new BattleStateAttackEnd());
        } else {
            Toast(_manager.chosenItem + " can't be used now!");
            Transition(new BattleStatePlayerStart());
        }        
        
        _manager.chosenItem = null;
        return base.execute(2f);
    }
}
