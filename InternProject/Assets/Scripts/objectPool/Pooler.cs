using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooler : MonoBehaviour
{
    [SerializeField] private GameObject spawnPrefab;
    [SerializeField] private int poolSize;
    [SerializeField] private bool expandAble;
    [SerializeField] private bool isRectTrans = false;

    private List<GameObject> freeList;
    private List<GameObject> usedList;

    private void Awake()
    {
        freeList = new List<GameObject>();
        usedList = new List<GameObject>();

        if (isRectTrans)
        {
            for (int i = 0; i < poolSize; i++)
            {
                GenerateNewObjectRect();
            }
        }

        else
        {
            /// normally this is the basic spawn but it have problem with ui element like dmg popup
            for (int i = 0; i < poolSize; i++)
            {
                GenerateNewObject();
            }
        }


    }

    public GameObject GetObject()
    {
        int totalFree = freeList.Count;
        if (totalFree == 0 && !expandAble) return null;
        else if (totalFree == 0 && expandAble) 
        {
            if (isRectTrans)
            {
                GenerateNewObjectRect();
            }
            else
            {
                GenerateNewObject();
            }
            
        }
        totalFree = freeList.Count;  // update again to prevent bug
        GameObject g = freeList[totalFree - 1];
        freeList.RemoveAt(totalFree - 1);
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

    private void GenerateNewObjectRect()
    {
        GameObject g = Instantiate(spawnPrefab);
        g.transform.SetParent(transform);
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

    public void SpawnBullet(Vector3 position, Quaternion rotation, int damage)
    {
        GameObject gameObject = GetObject();
        gameObject.GetComponent<DamageSetter>().Damage = damage;
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        gameObject.SetActive(true);
    }

    public void SpawnPopup(Vector3 position, Quaternion rotation, int damage, string type)
    {
        GameObject gameObject = GetObject();

        DamagePopup damagePopup = gameObject.GetComponent<DamagePopup>();
        damagePopup.Setup(damage, type);

        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        gameObject.SetActive(true);
    }

    public void SpawnGold(Vector3 position, Quaternion rotation, Gold gold)
    {
        GameObject gameObject = GetObject();
        gameObject.GetComponent<AssignGold>().setGold(gold);
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        gameObject.SetActive(true); 
    }


}
