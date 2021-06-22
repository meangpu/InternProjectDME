using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameSaveManager : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private ScriptableObject objectToSave;
    [SerializeField] private string filename;

    private string SavePath { get
        {
#if UNITY_EDITOR
            return Application.persistentDataPath + "/editor_save";
#else
            return Application.persistentDataPath + "/game_save";
#endif
        }
    }

    public bool IsSaveFile()
    {
        return Directory.Exists(SavePath);
    }

    public void SaveGame()
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(SavePath);
        }

        if (!Directory.Exists($"{SavePath}/player_stats"))
        {
            Directory.CreateDirectory($"{SavePath}/player_stats");
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create($"{SavePath}/player_stats/{filename}.txt");
        var json = JsonUtility.ToJson(objectToSave);
        bf.Serialize(file, json);
        file.Close();
    }

    public void LoadGame()
    {
        if (!Directory.Exists($"{SavePath}/player_stats"))
        {
            Directory.CreateDirectory($"{SavePath}/player_stats");
        }

        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists($"{SavePath}/player_stats/{filename}.txt"))
        {
            FileStream file = File.Open($"{SavePath}/player_stats/{filename}.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), objectToSave);
            file.Close();
        }
    }
}
