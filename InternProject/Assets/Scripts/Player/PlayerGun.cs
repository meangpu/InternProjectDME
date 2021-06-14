using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Transform barrel = null; // Bullet Spawn point
    [SerializeField] private PlayerStats playerStats = null;

    private bool canShoot = true; // Check if the player can shoot between shots
    private bool holdOnShoot = false;  // Check if the player is holding down shoot button to continuously shoot.
    private bool isReloading = false; // Check if the player is reloading to prevent ghost reloads.

    public UnityEvent OnShoot;

    private void OnEnable()
    {
        ResetBooleans();
    }

    private void Update()
    {
        if (holdOnShoot) { Shoot(); } // Shoot continuously while shoot button is held down.
    }

    private void Shoot() 
    {
        if (playerStats.GetCurrentAmmoCount() < 1) // If ammo is depleted and the player attempts to shoot, do an auto reload.
        {
            StartCoroutine(Reload(-4.5f));
            return;
        }

        if (!canShoot) { return; }

        // Shoot normally
        SpawnBullet();
    }

    public void ShootSpecial()
    {
        if (playerStats.GetCurrentAmmoCount() < 2)
        {
            if (playerStats.GetCurrentAmmoCount() == 0)
            {
                StartCoroutine(Reload(-4.5f));
            }
            return;
        }

        if (!canShoot) { return; }

        SpawnBullet(1.5f, 2f, 2);
    }

    private void SpawnBullet(float damageMultiplier = 1f, float speedMultiplier = 1f, int ammoUsed = 1)
    {
        OnShoot?.Invoke();

        PoolingSingleton.Instance.PlayerBulletPool.SpawnPlayerBullet(
            barrel.position,
            barrel.rotation,
            (int)(playerStats.DealDamage() * damageMultiplier),
            playerStats.GetBulletSpeed() * speedMultiplier,
            playerStats.GetBulletLifetime(),
            playerStats.GetKnockbackValue(),
            playerStats.GetBulletType());
        StartCoroutine(StartShootCooldown(playerStats.GetCoolDownBetweenShots(), ammoUsed));
    }

    public void OnHoldShootButton() // If shoot button is held down.
    {
        holdOnShoot = true;
    }

    public void OnReleaseShootButton() // If shoot button is released.
    {
        holdOnShoot = false;
    }

    private IEnumerator StartShootCooldown(float cooldownTime, int ammoUsed = 1)
    {
        playerStats.DecreaseCurrentAmmoCount(ammoUsed);
        canShoot = false;
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }

    public IEnumerator Reload(float percentageShortened = 0f)
    {
        if (isReloading) { yield break; } // in IEnumerator, yield break = return;
     
        isReloading = true;
        float reloadTime = playerStats.GetReloadTime() * ((100 - percentageShortened) / 100);
        UIManager.Instance.Reload(reloadTime);
        playerStats.EmptyCurrentAmmoCount();
        yield return new WaitForSeconds(reloadTime);
        playerStats.ReloadAmmoCount();
        isReloading = false;
    }

    public void ResetBooleans()
    {
        canShoot = true;
        holdOnShoot = false;
        isReloading = false;
    }
}
