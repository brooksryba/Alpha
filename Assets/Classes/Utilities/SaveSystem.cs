using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem {

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

}