using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Transform barrel = null; // Bullet Spawn point
    [SerializeField] private PlayerStats playerStats = null;

    private bool canShoot = true; // Check if the player can shoot between shots
    private bool holdOnShoot = false;  // Check if the player is holding down shoot button to continuously shoot.
    private bool isReloading = false; // Check if the player is reloading to prevent ghost reloads.

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
        PoolingSingleton.Instance.PlayerBulletPool.SpawnPlayerBullet(
            barrel.position, 
            barrel.rotation, 
            playerStats.DealDamage(), 
            playerStats.GetBulletSpeed(), 
            playerStats.GetBulletLifetime(), 
            playerStats.GetKnockbackValue(), 
            playerStats.GetBulletType());
        StartCoroutine(StartShootCooldown(playerStats.GetCoolDownBetweenShots()));
 
    }

    public void ShootSpecial()
    {
        PoolingSingleton.Instance.PlayerBulletPool.SpawnPlayerBullet(
            barrel.position,
            barrel.rotation,
            playerStats.DealDamage(),
            playerStats.GetBulletSpeed(),
            playerStats.GetBulletLifetime(),
            playerStats.GetKnockbackValue(),
            playerStats.GetBulletType());
        PoolingSingleton.Instance.PlayerBulletPool.SpawnPlayerBullet(
            barrel.position,
            barrel.rotation * Quaternion.Euler(0, 0, 3),
            playerStats.DealDamage(),
            playerStats.GetBulletSpeed(),
            playerStats.GetBulletLifetime(),
            playerStats.GetKnockbackValue(),
            playerStats.GetBulletType());
        PoolingSingleton.Instance.PlayerBulletPool.SpawnPlayerBullet(
            barrel.position,
            barrel.rotation * Quaternion.Euler(0, 0, -3),
            playerStats.DealDamage(),
            playerStats.GetBulletSpeed(),
            playerStats.GetBulletLifetime(),
            playerStats.GetKnockbackValue(),
            playerStats.GetBulletType()); 
    }

    public void OnHoldShootButton() // If shoot button is held down.
    {
        holdOnShoot = true;
    }

    public void OnReleaseShootButton() // If shoot button is released.
    {
        holdOnShoot = false;
    }

    private IEnumerator StartShootCooldown(float cooldownTime)
    {
        playerStats.DecreaseCurrentAmmoCount();
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
}
