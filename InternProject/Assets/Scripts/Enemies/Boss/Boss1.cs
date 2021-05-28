using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float dashSpeed;
    [SerializeField] float CoolDownBetweenDash;
    bool canDash = true;

    public void warnBoss(GameObject _bossObj)
    {
        
        var allBullet = GameObject.FindGameObjectsWithTag("PlayerBullet");
        foreach (var bullet in allBullet)
        {
            TankBullet bulScript = bullet.GetComponent<TankBullet>();
            Vector2  bulletDirection = (bulScript.GetRB().velocity).normalized;
            float projectionDistance = 0.4f;

            Vector2 BulletStartLine = (Vector2)bullet.transform.position + (Vector2)bullet.transform.up*projectionDistance;

            RaycastHit2D hit = Physics2D.Raycast(BulletStartLine, bulletDirection, 3f);

            Debug.DrawRay(BulletStartLine, bulletDirection*3f, Color.green);

            if(hit.collider != null)
            {
                Debug.Log(hit);
                Dash(bulletDirection);
            }

        }
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
    }


    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.TryGetComponent(out TankBullet bullet))
        {
            
        }
    }
    private IEnumerator OnDashCooldown()
    {
        yield return new WaitForSeconds(CoolDownBetweenDash);
        canDash = true;
    }


    private void FixedUpdate() 
    {
        warnBoss(gameObject);
    }

    [ContextMenu("sdasdas")]
    void Dash(Vector2 direction)
    {   
        if (!canDash) { return; }

        int ranDir;
        if (Random.value > 0.5f)
        {
            ranDir = 1;
        }
        else
        {
            ranDir = -1;
        }
        
        Quaternion rotation = Quaternion.Euler(0, 90 * ranDir, 1);  // create 90 degree rotation
        Vector2 dodgeVector = rotation * direction;
        rb.AddForce(dodgeVector * dashSpeed, ForceMode2D.Impulse);
        canDash = false;
        StartCoroutine(OnDashCooldown());

        // rb.velocity = Vector2.zero;
        
    }
}
