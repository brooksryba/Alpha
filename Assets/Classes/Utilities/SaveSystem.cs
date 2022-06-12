using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static Dictionary<string, Action> references = new Dictionary<string, Action>();
    
    public static void Reset()
    {
        foreach (var path in Directory.GetFiles(Application.persistentDataPath))
        {
            if( path.Contains(".bin") ) {
                FileInfo file = new FileInfo(path);
                file.Delete();
            }
        }
    }

    public static void SaveState<T>(T data, string name)
    {
        string PATH_SAVEFILE = Application.persistentDataPath + "/state-"+name+".bin";
        FileStream stream = new FileStream(PATH_SAVEFILE, FileMode.Create);
        
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static T LoadState<T>(string name) where T : class
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

    public static void Register(string name, Action saver) {
        if(!references.ContainsKey(name))
            references.Add(name, saver);
    }

    public static void Save() {
        foreach( KeyValuePair<string, Action> item in references ) {
            item.Value();
        }
    }

}