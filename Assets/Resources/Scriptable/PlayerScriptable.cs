using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable/PlayerScriptable")]
public class PlayerScriptable : ScriptableObject
{
    public bool ready = false;
    public Vector3 position;

    public void Write(Vector3 pos) {
        position = pos;
        ready = true;
    }

    public Vector3 Read() {
        ready = false;
        return position;
    }

}
