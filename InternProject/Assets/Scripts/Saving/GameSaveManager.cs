using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameSaveManager : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private List<ScriptableObject> objectsToSave;
    [SerializeField] private string folderName;
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

        if (!Directory.Exists($"{SavePath}/{folderName}"))
        {
            Directory.CreateDirectory($"{SavePath}/{folderName}");
        }

        for (int i = 0; i < objectsToSave.Count; i++)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create($"{SavePath}/{folderName}/{filename}_{i}.txt");
            var json = JsonUtility.ToJson(objectsToSave[i]);
            bf.Serialize(file, json);
            file.Close();
        }
    }

    public void LoadGame()
    {
        if (!Directory.Exists($"{SavePath}/{folderName}"))
        {
            Directory.CreateDirectory($"{SavePath}/{folderName}");
        }

        BinaryFormatter bf = new BinaryFormatter();

        for (int i = 0; i < objectsToSave.Count; i++)
        {
            if (File.Exists($"{SavePath}/{folderName}/{filename}_{i}.txt"))
            {
                FileStream file = File.Open($"{SavePath}/{folderName}/{filename}_{i}.txt", FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), objectsToSave[i]);
                file.Close();
            }
        }
    }
}
