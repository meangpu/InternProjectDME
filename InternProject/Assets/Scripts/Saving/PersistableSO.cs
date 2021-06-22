using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class PersistableSO : MonoBehaviour
{
    [Header("Meta")]
    [SerializeField] private string persisterName;
    [Header("Scriptable Objects")]
    [SerializeField] private List<ScriptableObject> objectsToPersist;

    private void OnEnable()
    {
        for (int i = 0; i < objectsToPersist.Count; i++)
        {
            string filename = Application.persistentDataPath + string.Format("/{0}_{1}.txt", persisterName, i);

            if (File.Exists(filename))
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Open(filename, FileMode.Open);
                JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), objectsToPersist[i]);
                file.Close();
            }
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < objectsToPersist.Count; i++)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + string.Format("/{0}_{1}.txt", persisterName, i));
            var json = JsonUtility.ToJson(objectsToPersist[i]);
            bf.Serialize(file, json);
            file.Close();
        }
    }
}
