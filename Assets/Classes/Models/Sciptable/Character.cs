using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Assets/New Character")]
public class Character : ScriptableObject
{
    public int characterID;
    public string title;
    public Condition condition;
    public Archetype archetype;

}
