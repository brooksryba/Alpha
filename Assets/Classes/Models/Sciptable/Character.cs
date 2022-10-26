using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Assets/New Character")]
public class Character : ScriptableObject
{
    public string characterID;
    public string title;
    public Condition condition;
    public Archetype archetype;

}
