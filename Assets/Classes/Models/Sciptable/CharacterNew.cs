using UnityEngine;

[CreateAssetMenu(fileName = "New Character", menuName = "Assets/New Character")]
public class CharacterNew : ScriptableObject
{
    public string title;
    public Condition condition;
    public Archetype archetype;

}
