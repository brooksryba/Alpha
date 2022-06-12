using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BattleStateSpellAnimationApproach : BattleState
{
    public bool createSpellObj = true;

    override public IEnumerator execute()
    {
        newState = this;

        if(createSpellObj){
            createSpellObj = false;
            _manager.spellPrefab = Resources.Load("Prefabs/Spells/" + _manager.chosenBattleMove) as GameObject;
            if(_manager.spellPrefab){
                _manager.spellObj = GameObject.Instantiate(_manager.spellPrefab, _manager.attacker.transform);
            } else {
                _manager.spellPrefab = Resources.Load("Prefabs/Spells/SpellOrb") as GameObject;
                _manager.spellObj = GameObject.Instantiate(_manager.spellPrefab, _manager.attacker.transform);
            }

        }
        
        BattleMovement battleMovement = _manager.spellObj.GetComponent<BattleMovement>();

        if(battleMovement && !battleMovement.isFinished && !battleMovement.isAnimating) {
            Vector3 offset = new Vector3(0, 0, 0);
            int flipInt = (_manager.playerParty.Contains(_manager.attacker.GetComponent<Character>().title)) ? 1 : -1;
            Vector3 targetPosition = new Vector3();
            _manager.spellObj.transform.position = _manager.spellObj.transform.position + new Vector3(flipInt*1f, 0.0f, 0.0f);
            if(_manager.defender) {
                targetPosition = _manager.defender.transform.position;
            }
            else {
                targetPosition = offset + new Vector3(0, 2, 0);
            }
            battleMovement.moveSpeed = 10.0f;
            battleMovement.Animate(_manager.spellObj.transform.position, targetPosition - offset); 

        }


        if(battleMovement == null || (!battleMovement.isAnimating && battleMovement.isFinished)) {
            if(battleMovement) battleMovement.Reset();
            newState = new BattleStateSpellAnimationEffects();
            
            yield return newState;
        }
    }
}
