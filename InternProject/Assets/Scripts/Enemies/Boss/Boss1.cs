using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    
    [SerializeField] BoxCollider2D boxCollider = null;
    [SerializeField] Rigidbody2D rb = null;
    [SerializeField] float dashSpeed;

    public static void warnBoss(GameObject _bossObj)
    {
        
        var allBullet = GameObject.FindGameObjectsWithTag("PlayerBullet");
        foreach (var bullet in allBullet)
        {
            TankBullet bulScript = bullet.GetComponent<TankBullet>();
            Vector2  bulletDirection = (bulScript.GetRB().velocity).normalized;
            float projectionDistance = 1f;
            Vector2 projectBulletPos = (Vector2)bullet.transform.position + bulletDirection* projectionDistance;

            Debug.DrawLine((Vector2)bullet.transform.position, projectBulletPos, Color.green);
            BoxCollider2D bulBox = bulScript.GetBox();
            bulBox.enabled = true;



            // m_LineRenderer = gameObject.AddComponent<LineRenderer> ();

            // Enemy projectCollision = (bullet.transform.position, projectBulletPos);

            // if (projectCollision != null)
            // {
            //     projectCollision.Warn(bullet.direction);
            // }

        }
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.TryGetComponent(out TankBullet bullet))
        {
            
        }
    }

    void Update()
    {
        warnBoss(gameObject);
    }


    void Dash()
    {
        rb.velocity = (Vector2)transform.up * -dashSpeed;
        
    }
}
