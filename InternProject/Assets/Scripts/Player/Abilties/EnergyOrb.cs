using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyOrb : MonoBehaviour
{
    [SerializeField] private Transform orbPivot = null;
    [SerializeField] private Rigidbody2D rb = null;

    private bool isActive;

    private float lifetime = 0;
    private float duration;
    private int damage;
    private float maxRange;
    private int energyPerHit;

    private void Update()
    {
        if (!isActive) { return; }

        float deltaTime = Time.deltaTime;

        lifetime += deltaTime;

        rb.velocity = transform.up * maxRange;
        orbPivot.Rotate(0, 0, 240 * deltaTime);

        if (lifetime > duration)
        {
            lifetime = 0f;
            isActive = false; 
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {   
        transform.position = orbPivot.position;
        isActive = true;     
    }

    public void Setup(int damage, float maxRange, float duration, int energyPerHit)
    {
        this.damage = damage;
        this.maxRange = maxRange;
        this.duration = duration;
        this.energyPerHit = energyPerHit;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IEnemy enemy))
        {
            enemy.TakeDamage(damage);
            PlayerStats.Instance.GetEnergySystem().Heal(energyPerHit);
        }
    }
}
