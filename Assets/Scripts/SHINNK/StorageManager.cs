using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Dictionary<string, string> todoList = new Dictionary<string, string>();
}

public class StorageManager
{
    private static string path;

    static StorageManager()
    {
        path = Path.Combine(Application.dataPath, "Resources/Json/storageData.json");
    }

    public static Dictionary<string, string> GetAll()
    {
        if (!File.Exists(path))
        {
            return null;
        }

        string loadJson = File.ReadAllText(path);
        SaveData saveData = JsonUtility.FromJson<SaveData>(loadJson);
        return saveData?.todoList;
    }

    public static void Save(Dictionary<string, string> todoList)
    {
        SaveData saveData = new SaveData();
        saveData.todoList = todoList;

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(path, json);
    }
}
