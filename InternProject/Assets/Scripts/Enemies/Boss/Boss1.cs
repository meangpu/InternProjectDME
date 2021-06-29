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
    [SerializeField] Enemy enemyScpt;
    public bool isSecondForm;
    bool canDash = true;

    private void WarnBoss()
    {
        Collider2D[] allBullet = Physics2D.OverlapCircleAll(transform.position, bulletDetectionDistance, playerBulletLayer);

        if (allBullet.Length < 1) { return; }

        Dash();
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
    }

    private void Dash(/*Vector2 direction*/)
    {   
        if (!canDash) { return; }
        
        Vector2 direction = new Vector2(Random.Range(0f, 1f), Random.Range(0f, 1f)).normalized;

        Quaternion rotation = Quaternion.Euler(0, 1, Random.Range(0, 360));
        Vector3 dodgeVector = rotation * direction;
        transform.position = transform.position + dodgeVector * dashSpeed;
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

    public void undoImmortal()
    {
        enemyScpt.SetImmortalState(false);
    }
}
