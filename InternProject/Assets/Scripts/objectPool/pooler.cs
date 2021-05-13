using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private int poolSize;
    [SerializeField] private bool expandAble;

    private List<GameObject> freeList;
    private List<GameObject> usedList;

    private void Awake() 
    {
        freeList = new List<GameObject>();
        usedList = new List<GameObject>();

        for (int i=0; i < poolSize; i++)
        {
            GenerateNewObject();
        }
    }

    public GameObject GetObject()
    {
        int totalFree = freeList.Count;
        if(totalFree == 0 && !expandAble) return null;
        else if(totalFree == 0 && expandAble) GenerateNewObject();

        totalFree = freeList.Count;  // update again to prevent bug
        GameObject g = freeList[totalFree-1];
        freeList.RemoveAt(totalFree-1);
        usedList.Add(g);

        
        return g;
    }

    public void ReturnObject(GameObject obj)
    {
        // Debug.Assert(usedList.Contains(obj)); //check if there is obj in usedlist
        obj.SetActive(false);
        usedList.Remove(obj);
        freeList.Add(obj);
    }

    private void GenerateNewObject()
    {
        GameObject g = Instantiate(spawnPrefab);
        g.transform.parent = transform;
        g.SetActive(false);
        freeList.Add(g);
    }

    public void SpawnObject(Vector3 position, Quaternion rotation)
    {
        GameObject gameObject = GetObject();
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        gameObject.SetActive(true);
    }
}
