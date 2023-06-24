using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Archetype", menuName = "Assets/New Archetype")]
public class Archetype : ScriptableObject
{
    public string title;
    public (int, int) hp; // level 0 hp, increment per level
    public (int, int) mana;
    public (int, int) attackPhysical;
    public (int, int) attackMagic;
    public (int, int) defensePhysical;
    public (int, int) defenseMagic;
    public (int, int) speed;
    public List<(int, string)> attacks; // level, id of attack you receive
    public List<(int, string)> spells;

}
