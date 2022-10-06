using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Assets/New Item")]
public class Item : ScriptableObject
{
    public string title;
    public string verb;
    public int hpEffect;
    public int hpCost;
    public int manaEffect;
    public int manaCost;
    public enum type {both, world, battle};
    public enum target {all, self, friendly, enemy};
}
