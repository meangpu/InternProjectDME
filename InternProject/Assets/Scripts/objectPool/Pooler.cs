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

    public GameObject SpawnObject(Vector3 position, Quaternion rotation)
    {
        GameObject gameObject = GetObject();
        gameObject.transform.position = position;
        gameObject.transform.rotation = rotation;
        gameObject.SetActive(true);

        return gameObject;
    }

    public void SpawnEnemyBullet(Vector3 position, Quaternion rotation, int damage, float speed, float lifetime, ObjEnemyBullet _objBullet)
    {
        GameObject bullet = SpawnObject(position, rotation);
        bullet.GetComponent<EnemyBulletDisplay>().SetupBullet(_objBullet);
        IProjectile bulletStats = bullet.GetComponent<IProjectile>();
        bulletStats.Damage = damage;
        bulletStats.BulletSpeed = speed;
        bulletStats.Lifetime = lifetime;     
    }

    public void SpawnPlayerBullet(Vector3 position, Quaternion rotation, int damage, float speed, float lifetime, float knockBack, ObjPlayerBullet _objBullet)
    {
        GameObject bullet = SpawnObject(position, rotation);
        bullet.GetComponent<PlayerBulletDisplay>().SetupBullet(_objBullet);
        IProjectile bulletStats = bullet.GetComponent<IProjectile>();
        bulletStats.Damage = damage;
        bulletStats.BulletSpeed = speed;
        bulletStats.Lifetime = lifetime;
        bulletStats.KnockBack = knockBack;
    }

    public void SpawnPopup(Vector3 position, Quaternion rotation, int damage, DamagePopup.DamageType type)
    {
        GameObject popup = SpawnObject(position, rotation);

        DamagePopup damagePopup = popup.GetComponent<DamagePopup>();
        damagePopup.Setup(damage, type);
    }

    public void SpawnGold(Vector3 position, Quaternion rotation, ObjGold ObjGold)
    {
        GameObject goldItem = SpawnObject(position, rotation);
        goldItem.GetComponent<AssignGold>().SetGold(ObjGold);
    }


}
