using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

    public static void Reset()
    {
        foreach (var path in Directory.GetFiles(Application.persistentDataPath))
        {
            FileInfo file = new FileInfo(path);
            file.Delete();
        }
    }

    public static void SaveStatePlayer(Player player)
    {
        string PATH_SAVEFILE = Application.persistentDataPath + "/state-player.bin";
        FileStream stream = new FileStream(PATH_SAVEFILE, FileMode.Create);
        
        PlayerData data = new PlayerData(player);

        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadStatePlayer()
    {
        string PATH_SAVEFILE = Application.persistentDataPath + "/state-player.bin";
        if( File.Exists(PATH_SAVEFILE) ) {
            FileStream stream = new FileStream(PATH_SAVEFILE, FileMode.Open);

            BinaryFormatter formatter = new BinaryFormatter();
            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        } else {
            return null; 
        }
    }

    public static void SaveStateEnemy(Enemy enemy, string name)
    {
        string PATH_SAVEFILE = Application.persistentDataPath + "/state-enemy-"+ name +".bin";
        FileStream stream = new FileStream(PATH_SAVEFILE, FileMode.Create);
        
        EnemyData data = new EnemyData(enemy);

        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static EnemyData LoadStateEnemy(string name)
    {
        string PATH_SAVEFILE = Application.persistentDataPath + "/state-enemy-"+ name +".bin";
        if( File.Exists(PATH_SAVEFILE) ) {
            FileStream stream = new FileStream(PATH_SAVEFILE, FileMode.Open);

            BinaryFormatter formatter = new BinaryFormatter();
            EnemyData data = formatter.Deserialize(stream) as EnemyData;
            stream.Close();

            return data;
        } else {
            return null; 
        }
    }
}