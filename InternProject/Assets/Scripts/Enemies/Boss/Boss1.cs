using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float dashSpeed;
    [SerializeField] float CoolDownBetweenDash;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Material secondFormMat;
    [SerializeField] float secondFormDashCdMultiplyer;
    public bool isSecondForm;
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

            // Debug.DrawRay(BulletStartLine, bulletDirection*3f, Color.green);

            if(hit.collider != null)
            {
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
        rb.velocity = Vector2.zero;
        canDash = true;
    }


    private void FixedUpdate() 
    {
        if (!canDash) { return; }
        warnBoss(gameObject);
    }

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
        
        Quaternion rotation = Quaternion.Euler(0, 1, 90 * ranDir);  // create 90 degree rotation
        Vector3 dodgeVector = rotation * direction;
        transform.position = transform.position + dodgeVector * dashSpeed;
        animator.SetTrigger("Dash");
        // rb.velocity = (dodgeVector * dashSpeed);
        
        canDash = false;
        StartCoroutine(OnDashCooldown());

        
        
    }

    public void ChangeForm()
    {
        if (!isSecondForm)
        {
            isSecondForm = true;
            animator.SetTrigger("SecondForm");
            spriteRenderer.material = secondFormMat;
            CoolDownBetweenDash *= secondFormDashCdMultiplyer;
        }

    }

}
