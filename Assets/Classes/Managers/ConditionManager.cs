using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionManager
{
    public static Dictionary<string, Condition> refs = new Dictionary<string, Condition>();

    // Function to load in current values, if not use archetype to fill in
    public static void Load()
    {
        // Loads save file into dictionary
    }

    
    public static Condition Get(Character character)
    {
        if(!refs.ContainsKey(character.characterID)){
            Archetype archetype = character.archetype;

            Condition condition = ScriptableObject.CreateInstance<Condition>();
            condition.level = 5;
            condition.xp = 100;
            (int hpBase, int hpIncrement) = archetype.hp;
            condition.hp = (hpBase + hpIncrement * condition.level, hpBase + hpIncrement * condition.level);

            (int manaBase, int manaIncrement) = archetype.mana;
            condition.mana = (manaBase + manaIncrement * condition.level, manaBase + manaIncrement * condition.level);

            List<Move> attacks = new List<Move>();
            foreach((int attackLevel, string attackId) in archetype.attacks){
                attacks.Add(MoveManager.Get(attackId));
            }
            condition.attacks = attacks;

            List<Move> spells = new List<Move>();
            foreach((int spellLevel, string spellId) in archetype.spells){
                attacks.Add(MoveManager.Get(spellId));
            }
            condition.spells = spells;

            condition.items = new List<(Item, int)>();

            List<string> party = new List<string>();
            if(character.characterID=="_mCharacterHeroName"){
                party.Add("AF");
                party.Add("MF");
            }

            condition.party = party;

            refs.Add(character.characterID, condition);
        }
        return refs[character.characterID];
    }

    
}
