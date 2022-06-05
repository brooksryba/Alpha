using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStateAttackAnimationEffects : BattleState
{

    override public IEnumerator execute()
    {
        newState = this;

        GameObject effect = battleObjManager.attacker.transform.GetChild(0).Find(battleObjManager.chosenAttack).gameObject;

        if(effect) {
            Vector3 originalRotation = effect.transform.eulerAngles;
            if(battleObjManager.attackerPositionStart.x > 0) {
                effect.transform.eulerAngles = new Vector3(
                    effect.transform.eulerAngles.x,
                    effect.transform.eulerAngles.y ,
                    effect.transform.eulerAngles.z + 180
                );
            }
            effect.SetActive(true);
            yield return new WaitForSeconds(2f);
            effect.SetActive(false);
            effect.transform.eulerAngles = originalRotation;
        }
        battleObjManager.battleSystemHud.RefreshAllHUDs();

        Color DeathColor = new Color (0.25f, 0.25f, 0.25f, 0.25f);
        for(int i = 0; i < battleObjManager.playerParty.Count; i++){
            Character partyMember = battleSystemUtils.GetCharacter(battleObjManager.playerParty[i]);
            GameObject partyMemberObj = GameObject.Find(partyMember.title);
            if(partyMember.currentHP <= 0 && !battleObjManager.deadPlayerList.Contains(battleObjManager.playerParty[i])){
                battleObjManager.deadPlayerList.Add(battleObjManager.playerParty[i]);
                BattleSpriteController spriteController = partyMemberObj.GetComponent<BattleSpriteController>();
                spriteController.TransitionColors(spriteController.sprite.color, DeathColor, 3.0f);
            }
        }

        for(int i = 0; i < battleObjManager.enemyParty.Count; i++){
            Character partyMember = battleSystemUtils.GetCharacter(battleObjManager.enemyParty[i]);
            GameObject partyMemberObj = GameObject.Find(partyMember.title);
            if(partyMember.currentHP <= 0 && !battleObjManager.deadPlayerList.Contains(battleObjManager.enemyParty[i])){
                battleObjManager.deadPlayerList.Add(battleObjManager.enemyParty[i]);
                BattleSpriteController spriteController = partyMemberObj.GetComponent<BattleSpriteController>();
                spriteController.TransitionColors(spriteController.sprite.color, DeathColor, 3.0f);
            }
        }



        newState = new BattleStateAttackAnimationRetreat();
        yield return newState;
    }
}
