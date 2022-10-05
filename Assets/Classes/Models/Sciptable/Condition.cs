using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : ScriptableObject
{
    public (int, int) hp;
    public (int, int) mana;
    public int xp;
    public int level;

    // @TODO - Should attacks and spells be here or in the archetype?
    public List<int> attacks;
    public List<int> spells;
    public List<(int, int)> items;
    public List<int> party;
}
