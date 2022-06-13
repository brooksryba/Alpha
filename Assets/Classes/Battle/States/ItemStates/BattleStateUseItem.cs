using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateUseItem : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;
        _manager.battleSystemMenu.closeOptionSubmenu();
        _manager.battleSystemHud.canSelect = true;        

        if(BattleItems.lookup.ContainsKey(_manager.chosenItem)) {
            InventoryItemData itemData = BattleItems.lookup[_manager.chosenItem];
            Character activeCharacter = _manager.charManager.attacker.GetComponent<Character>();
            _manager.dialogueText.text = itemData.Execute(activeCharacter);
            yield return new WaitForSeconds(1.5f); 
            newState = new BattleStateAttackEnd();
        } else {
            _manager.dialogueText.text = _manager.chosenItem + " can't be used now!";
            yield return new WaitForSeconds(2f);
            newState = new BattleStatePlayerStart();
        }        
        
        _manager.chosenItem = null;
    }
}
