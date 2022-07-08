using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
public static class SaveSystem
{
    public static void SavePlayer(GeneralStats player, LevelSystem level, Transform charName)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/"+charName.name+".data";

        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player, level,charName);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer(Transform charName)
    {
        string path = Application.persistentDataPath + "/" + charName.name + ".data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    public static void SaveProgression(Progression pro, FTUE_Progresion fTUE_Progresion)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/progression.data";

        FileStream stream = new FileStream(path, FileMode.Create);

        GameProgressionData data = new GameProgressionData(pro, fTUE_Progresion);

        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static GameProgressionData LoadProgression()
    {
        string path = Application.persistentDataPath + "/progression.data";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stram = new FileStream(path, FileMode.Open);

            GameProgressionData data = formatter.Deserialize(stram) as GameProgressionData;
            stram.Close();
            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
