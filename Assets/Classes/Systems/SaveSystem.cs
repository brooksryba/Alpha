using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveSystem : MonoBehaviour {

    private static SaveSystem _instance;
    public static SaveSystem instance { get { return _instance; } }

    public Dictionary<string, Action> references = new Dictionary<string, Action>();
    public BattleSceneScriptable battleSceneScriptable;
    public PlayerScriptable playerScriptable;

    private void Awake() { _instance = this; }
    
    public void Reset()
    {
        foreach (var path in Directory.GetFiles(Application.persistentDataPath))
        {
            if( path.Contains(".bin") && !path.Contains("SettingsData") ) {
                FileInfo file = new FileInfo(path);
                file.Delete();
            }
        }

        battleSceneScriptable.enemy = null;
        battleSceneScriptable.scene = null;
        battleSceneScriptable.scenePath = null;
        
    }

    public void SaveState<T>(T data, string name)
    {
        string PATH_SAVEFILE = Application.persistentDataPath + "/state-"+name+".bin";
        FileStream stream = new FileStream(PATH_SAVEFILE, FileMode.Create);
        
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public T LoadState<T>(string name) where T : class
    {
        string PATH_SAVEFILE = Application.persistentDataPath + "/state-"+name+".bin";
        if( File.Exists(PATH_SAVEFILE) ) {
            FileStream stream = new FileStream(PATH_SAVEFILE, FileMode.Open);

            BinaryFormatter formatter = new BinaryFormatter();
            T data = formatter.Deserialize(stream) as T;
            stream.Close();

            return data;
        } else {
            return null; 
        }
    }

    public void Deregister() {
        references = new Dictionary<string, Action>();
    }

    public void Register(string name, Action saver) {
        if(!references.ContainsKey(name))
            references.Add(name, saver);
    }

    public void Save() {
        foreach( KeyValuePair<string, Action> item in references ) {
            item.Value();
        }
    }

    public void SaveAndDeregister() {
        Save();
        Deregister();
    }

    public void ResetAndDeregister() {
        Reset();
        Deregister();
    }    

}