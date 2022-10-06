using UnityEngine;

[CreateAssetMenu(fileName = "New Battle Effect", menuName = "Assets/New Battle Effect")]
public class BattleEffect : ScriptableObject
{
    public string title;
    public string verb;
    public int duration;
    public (int, int) value; // multiplicative, additive
    public enum type {speed, hp, mana, attackPhysical, attackMagic, defensePhysical, defenseMagic, skipTurn, skipTurnChange};
}
