using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    [SerializeField] private LayerMask playerBulletLayer;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float dashSpeed;
    [SerializeField] float CoolDownBetweenDash;
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Material secondFormMat;
    [SerializeField] float secondFormDashCdMultiplyer;
    [SerializeField] private float bulletDetectionDistance = 2.5f;
    public bool isSecondForm;
    bool canDash = true;

    private void WarnBoss()
    {
        Collider2D[] allBullet = Physics2D.OverlapCircleAll(transform.position, bulletDetectionDistance, playerBulletLayer);

        if (allBullet.Length < 1) { return; }

        Dash();
        /*foreach (Collider2D bullet in allBullet)
        {
            TankBullet bulScript = bullet.GetComponent<TankBullet>();
            Vector2 bulletDirection = (bulScript.GetRB().velocity).normalized;
            float projectionDistance = 0.4f;

            Vector2 BulletStartLine = (Vector2)bullet.transform.position + (Vector2)bullet.transform.up*projectionDistance;

            RaycastHit2D hit = Physics2D.Raycast(BulletStartLine, bulletDirection, 3f);

            // Debug.DrawRay(BulletStartLine, bulletDirection*3f, Color.green);

            if(hit.collider != null)
            {
                Dash(bulletDirection);
            }

        }*/
    }

    private void OnDrawGizmos() 
    {
        Gizmos.color = Color.red;
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

        WarnBoss();

        // DashForward();
    }

    private void Dash(/*Vector2 direction*/)
    {   
        if (!canDash) { return; }
        
        Vector2 direction = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)).normalized;

        Quaternion rotation = Quaternion.Euler(0, 1, Random.Range(0, 360));  // create 90 degree rotation
        Vector3 dodgeVector = rotation * direction;
        transform.position = transform.position + dodgeVector * dashSpeed;
        animator.SetTrigger("Dash");
        // rb.velocity = (dodgeVector * dashSpeed);
        
        canDash = false;
        StartCoroutine(OnDashCooldown());
    }

    private void DashForward()
    {
        if (!canDash) { return; }

        // find dash direction

        transform.position = transform.position + Vector3.down * dashSpeed;

        animator.SetTrigger("Dash");
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
