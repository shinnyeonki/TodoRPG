using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class StorageManager
{

    private static string masterKey = "e82d33663b9995d23cd93a7a68b782";

    [Serializable]
    public class TodoItem
    {
        public DateTime CreationDate { get; set; }
        public DateTime DueDate { get; set; }
        public string Text { get; set; }
        public bool IsPriority { get; set; }
    }

    public static Dictionary<string, TodoItem> GetAll()
    {
        if (!PlayerPrefs.HasKey(masterKey))
        {
            return null;
        }
        string masterKeyValue = PlayerPrefs.GetString(masterKey);
        return Deserialize<Dictionary<string, TodoItem>>(masterKeyValue); //역직렬화 해서 반환 즉 저장된 데이터를 string + 객체 = dictionary로 반환
    }

    public static void Save(Dictionary<string, TodoItem> todoList)
    {
        PlayerPrefs.SetString(masterKey, Serialize(todoList));
    }

    // > Saving a Dictionary To PlayerPrefs - Unity Answers  
    // > https://answers.unity.com/questions/1022800/saving-a-dictionary-to-playerprefs.html
    // > How do I save a custom class of variables to playerprefs? - Unity Answers  
    // > https://answers.unity.com/questions/610893/how-do-i-save-a-custom-class-of-variables-to-playe.html?_ga=2.36768227.212595546.1570296762-888141676.1564340563
    private static T Deserialize<T>(string value) where T : class
    {
        byte[] bytes = Convert.FromBase64String(value);
        MemoryStream stream = new MemoryStream(bytes);
        BinaryFormatter formatter = new BinaryFormatter();
        return formatter.Deserialize(stream) as T;
    }

    private static string Serialize<T>(T data) where T : class
    {
        // > c# - binary formatter to string - Stack Overflow  
        // > https://stackoverflow.com/questions/10903327/binary-formatter-to-string
        var memoryStream = new MemoryStream();
        var formatter = new BinaryFormatter();
        using (memoryStream)
        {
            formatter.Serialize(memoryStream, data);
        }
        return Convert.ToBase64String(memoryStream.ToArray());
    }
}
