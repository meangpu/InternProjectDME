using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveStars : MonoBehaviour
{
    private readonly string folderName = "level_progression";
    private readonly string filename = "leveldata";

    private string SavePath
    {
        get
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

    public void SaveGame(ObjLevel level)
    {
        if (!IsSaveFile())
        {
            Directory.CreateDirectory(SavePath);
        }

        if (!Directory.Exists($"{SavePath}/{folderName}"))
        {
            Directory.CreateDirectory($"{SavePath}/{folderName}");
        }

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create($"{SavePath}/{folderName}/{filename}_{level.ID}.txt");
        var json = JsonUtility.ToJson(level.Stats);
        bf.Serialize(file, json);
        file.Close();
    }
}