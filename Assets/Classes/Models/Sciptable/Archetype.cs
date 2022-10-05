using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Archetype", menuName = "Assets/New Archetype")]
public class Archetype : ScriptableObject
{
    public string title;
    public (int, int) hp;
    public (int, int) mana;
    public (int, int) attackPhysical;
    public (int, int) attackMagic;
    public (int, int) defensePhysical;
    public (int, int) defenseMagic;
    public (int, int) speed;
    public List<int> attacks;
    public List<int> spells;

}
