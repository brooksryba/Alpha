using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A seperate script on a character prefab could manage animations and such on status changes e.g.
//character.condition.onStatusChange += () => 
//                     onHealthChange
//                     .onManaChange
public class Condition : ScriptableObject
{
    public (int, int) hp;
    public (int, int) mana;
    public int xp;
    public int level;

    // @TODO - Should attacks and spells be here or in the archetype?
    public List<Move> attacks;
    public List<Move> spells;
    // @TODO - items might need to be a dictionary to make sure adding can be done more easily (especially handling multiple items)
    public List<(Item, int)> items;
    public List<int> party;
}
