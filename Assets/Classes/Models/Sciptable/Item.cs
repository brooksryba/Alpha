using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Assets/New Item")]
public class Item : ScriptableObject
{
    public string itemID;
    public string title;
    public string verb;
    public int hpEffect;
    public int hpCost;
    public int manaEffect;
    public int manaCost;
    public enum Type {both, world, battle};
    public Type type;
    public enum Target {all, self, friendly, enemy};
    public Target target;
}
