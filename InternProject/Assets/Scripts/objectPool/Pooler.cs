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

    public GameObject SpawnObject()
    {
        GameObject gameObject = GetObject();
        return gameObject;
    }

    private void SetupObject(GameObject gameObject, Vector3 position, Quaternion rotation) 
    {
        gameObject.transform.SetPositionAndRotation(position, rotation);
        gameObject.SetActive(true);
    }

    public void SpawnEnemy(Vector3 position, Quaternion rotation, ObjEnemy objEnemy)
    {
        GameObject enemy = SpawnObject();
        enemy.GetComponent<EnemyDisplay>().StartDisplay(objEnemy);

        /*if (enemy.TryGetComponent(out EnemyShoot enemyShoot))
        {
            enemyShoot.StartShooting();
        }*/
        
        SetupObject(enemy, position, rotation);
        WaveManager.EnemyAlive.Add(enemy.GetComponent<Enemy>());
    }

    public void SpawnEnemyDeathPar(Vector3 position, Quaternion rotation)
    {
        GameObject deathPar = SpawnObject();
        SetupObject(deathPar, position, rotation);
        deathPar.GetComponent<ParticleSystem>().Play();
    }


    public void SpawnEnemyBullet(Vector3 position, Quaternion rotation, int damage, float speed, float lifetime, ObjEnemyBullet _objBullet)
    {
        GameObject bullet = SpawnObject();
        bullet.GetComponent<EnemyBulletDisplay>().SetupBullet(_objBullet);
        IProjectile bulletStats = bullet.GetComponent<IProjectile>();
        bulletStats.Damage = damage;
        bulletStats.BulletSpeed = speed;
        bulletStats.Lifetime = lifetime;
        SetupObject(bullet, position, rotation);
    }

    public void SpawnPlayerBullet(Vector3 position, Quaternion rotation, int damage, float speed, float lifetime, float knockBack, ObjPlayerBullet _objBullet)
    {
        GameObject bullet = SpawnObject();
        bullet.GetComponent<PlayerBulletDisplay>().SetupBullet(_objBullet);
        IProjectile bulletStats = bullet.GetComponent<IProjectile>();
        bulletStats.Damage = damage;
        bulletStats.BulletSpeed = speed;
        bulletStats.Lifetime = lifetime;
        bulletStats.KnockBack = knockBack;
        SetupObject(bullet, position, rotation);
    }

    public void SpawnPlayerMissile(Vector3 position, Quaternion rotation, int damage, float speed, float range, float lifetime)
    {
        GameObject missile = SpawnObject();
        missile.GetComponent<HomingMissile>().Setup(HomingMissile.TargetType.Enemy);
        IAreaOfDamage missileStats = missile.GetComponent<IAreaOfDamage>();
        missileStats.Damage = damage;
        missileStats.BulletSpeed = speed;
        missileStats.AreaOfDamage = range;
        missileStats.Lifetime = lifetime;
        SetupObject(missile, position, rotation);
    }

    public void SpawnEnemyMissile(Vector3 position, Quaternion rotation, int damage, float speed, float lifetime)
    {
        GameObject missile = SpawnObject();
        missile.GetComponent<HomingMissile>().Setup(HomingMissile.TargetType.Player);
        IProjectile missileStats = missile.GetComponent<IProjectile>();
        missileStats.Damage = damage;
        missileStats.BulletSpeed = speed;
        missileStats.Lifetime = lifetime;
        SetupObject(missile, position, rotation);
    }

    public void SpawnTowerBullet(Vector3 position, Quaternion rotation, int damage, float speed, float lifetime)
    {
        GameObject bullet = SpawnObject();
        IProjectile bulletStats = bullet.GetComponent<IProjectile>();
        bulletStats.Damage = damage;
        bulletStats.BulletSpeed = speed;
        bulletStats.Lifetime = lifetime;
        SetupObject(bullet, position, rotation);
    }

    public void SpawnTowerMissile(Vector3 position, Quaternion rotation, int damage, float speed, float range, float lifetime)
    {
        GameObject missile = SpawnObject();
        IAreaOfDamage missileStats = missile.GetComponent<IAreaOfDamage>();
        missileStats.Damage = damage;
        missileStats.BulletSpeed = speed;
        missileStats.AreaOfDamage = range;
        missileStats.Lifetime = lifetime;
        SetupObject(missile, position, rotation);
    }

    public void SpawnPopup(Vector3 position, Quaternion rotation, int damage, DamagePopup.DamageType type)
    {
        GameObject popup = SpawnObject();

        DamagePopup damagePopup = popup.GetComponent<DamagePopup>();
        damagePopup.Setup(damage, type);

        SetupObject(popup, position, rotation);
    }

    public void SpawnGold(Vector3 position, Quaternion rotation, ObjGold ObjGold, Vector3 _direction)
    {
        GameObject goldItem = SpawnObject();
        goldItem.GetComponent<AssignGold>().SetGold(ObjGold);
        SetupObject(goldItem, position, rotation);
        goldItem.GetComponent<Rigidbody2D>().AddForce(_direction * 0.0015f, ForceMode2D.Impulse);
    }
}
