using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Systems))]
public class SystemsInspector : Editor
{
    public int selected = 0;
    public List<string> files = new List<string>();

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUILayout.Label("", GUI.skin.horizontalSlider);
        GUILayout.Label("");
        GUILayout.Label("Fixtures");

        files.Clear();
        foreach (TextAsset asset in Resources.LoadAll("Fixtures", typeof(TextAsset))) {
           files.Add(asset.name);
        }

        selected = EditorGUILayout.Popup("Load File: ", selected, files.ToArray()); 
         
        GUILayout.BeginHorizontal();

        if(GUILayout.Button("Import")) {
            TextAsset file = Resources.Load("Fixtures/"+files[selected]) as TextAsset; 
            SaveData extract = JsonUtility.FromJson<SaveData>(file.text);
            extract.ToDisk();
            Debug.Log("Import data");
        }

        if(GUILayout.Button("Reset")) {
            SaveSystem.Reset();
            Debug.Log("Reset data");
        }

        GUILayout.EndHorizontal();
    }
}
