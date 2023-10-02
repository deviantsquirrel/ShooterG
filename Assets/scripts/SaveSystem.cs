using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveSystem 
{
    public static void SaveStats(Dictionary<string, int> myDictionary, Dictionary<int, string> myDictionary_Positions, float health)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.fun";
        FileStream stream = new FileStream(path, FileMode.Create);

        Playerdata data = new Playerdata(myDictionary, myDictionary_Positions, health); 
        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static Playerdata LoadStats()
    {
        string path = Application.persistentDataPath + "/player.fun";
        Debug.Log(path);
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            Playerdata data = formatter.Deserialize(stream) as Playerdata;
            stream.Close();

            return data;
        }
        else
        {
            //Debug.LogError("SaveFile not found in "+ path);
            Debug.Log("No file");
            return null;
        }
    }
}
