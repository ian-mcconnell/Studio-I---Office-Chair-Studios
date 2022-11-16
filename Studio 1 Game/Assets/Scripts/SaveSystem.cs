using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public  class SaveSystem 
{
    public static  List<Item> items = new List<Item>();
    public static void SavePlayer (PlayerController player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.data";
        Debug.Log(Application.persistentDataPath);
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
    public static void SaveItem(Item item)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath  + "/Item.data" + SceneManager.GetActiveScene().name ;
        string countpath = Application.persistentDataPath + "/Item.count" + SceneManager.GetActiveScene().name;

        FileStream countStream = new FileStream(countpath, FileMode.Create);
        formatter.Serialize(countStream, items.Count);
        countStream.Close();
        for (int i = 0; i < items.Count; i++)
        {
            FileStream stream = new FileStream(path + i, FileMode.Create);
            ItemData data = new ItemData(items[i]);
            formatter.Serialize(stream, data);
            stream.Close();
        }
        


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
    public static ItemData loadItem()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/Item.data" + SceneManager.GetActiveScene().name;
        string countpath = Application.persistentDataPath + "/Item.count" + SceneManager.GetActiveScene().name;
        int itemCount = 0;
        if (File.Exists(countpath))
        {


            //    Debug.Log(path);

            FileStream stream = new FileStream(countpath, FileMode.Open);
            itemCount = (int)formatter.Deserialize(stream);
            //ItemData data = formatter.Deserialize(stream) as ItemData;
            stream.Close();
            for (int i = 0; i < itemCount; i++)
            {
                if (File.Exists(path + i))
                {
                FileStream countstream = new FileStream(path + i, FileMode.Open);
                ItemData data = formatter.Deserialize(countstream) as ItemData;
                countstream.Close();

                //Item item = Instantiate()
                return data;
                }
                else
                {
                Debug.LogError("Save file not found in " + path + i);
                return null;
                 }

            }
            return null;

        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }

    }
}
