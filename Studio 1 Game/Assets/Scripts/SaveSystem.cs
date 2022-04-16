using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem 
{
    public static void SavePlayer (PlayerController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static void SaveInv(InventorySystem inventory)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Inventory.data";
        FileStream stream = new FileStream(path, FileMode.Create);

        InvData data = new InvData(inventory);
        formatter.Serialize(stream, data);
        stream.Close();
    }
    public static PlayerData loadPlayer()
    {
        string path = Application.persistentDataPath + "/player.data";
        if (File.Exists(path))
        {
        //    Debug.Log(path);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;

        }else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
    public static InvData loadInv()
    {
        string path = Application.persistentDataPath + "/Inventory.data";
        if (File.Exists(path))
        {
            //    Debug.Log(path);
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            InvData data = formatter.Deserialize(stream) as InvData;
            stream.Close();

            return data;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }
    }
}
