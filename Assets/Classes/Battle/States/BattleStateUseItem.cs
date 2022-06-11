using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStateUseItem : BattleState
{
    override public IEnumerator execute()
    {
        newState = this;
        battleObjManager.battleSystemMenu.closeOptionSubmenu();
        battleObjManager.battleSystemHud.canSelect = true;        

        if(BattleItems.lookup.ContainsKey(battleObjManager.chosenItem)) {
            InventoryItemData itemData = BattleItems.lookup[battleObjManager.chosenItem];
            Character activeCharacter = battleObjManager.attacker.GetComponent<Character>();
            battleObjManager.dialogueText.text = itemData.Execute(activeCharacter);
            yield return new WaitForSeconds(1.5f); 
            newState = new BattleStateAttackEnd();
        } else {
            battleObjManager.dialogueText.text = battleObjManager.chosenItem + " can't be used now!";
            yield return new WaitForSeconds(2f);
            newState = new BattleStatePlayerStart();
        }        
        
        battleObjManager.chosenItem = null;
    }
}
