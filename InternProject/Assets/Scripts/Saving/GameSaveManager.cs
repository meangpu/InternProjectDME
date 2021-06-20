using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameSaveManager : MonoBehaviour
{
    [SerializeField] private ScriptableObject objectToSave;

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
        FileStream file = File.Create(Application.persistentDataPath + "/game_save/player_stats/player.txt");
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

        if (File.Exists(Application.persistentDataPath + "/game_save/player_stats/player.txt"))
        {
            FileStream file = File.Open(Application.persistentDataPath + "/game_save/player_stats/player.txt", FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), objectToSave);
            file.Close();
        }

    }
}
