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

    public bool IsSaveFile()
    {
        return Directory.Exists(Application.persistentDataPath + "/game_save");
    }

    public void SaveGame()
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save");
        }

        if (!Directory.Exists(Application.persistentDataPath + "/game_save/player_stats"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/player_stats");
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + $"/game_save/player_stats/{filename}.txt");
        var json = JsonUtility.ToJson(objectToSave);
        bf.Serialize(file, json);
        file.Close();
    }

    public void LoadGame()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/game_save/player_stats"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/game_save/player_stats");
        }

        BinaryFormatter bf = new BinaryFormatter();

        if (File.Exists(Application.persistentDataPath + $"/game_save/player_stats/{filename}.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + $"/game_save/player_stats/{filename}.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), objectToSave);
            file.Close();
        }
    }
}
