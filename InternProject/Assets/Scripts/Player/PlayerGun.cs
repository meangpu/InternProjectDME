using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGun : MonoBehaviour
{
    [SerializeField] private Transform barrel = null; // Bullet Spawn point
    [SerializeField] private Player player = null;

    private Pooler bulletPool;
    private ReloadBar reloadBar;

    private bool canShoot = true; // Check if the player can shoot between shots
    private bool holdOnShoot = false;  // Check if the player is holding down shoot button to continuously shoot.
    private bool isReloading = false; // Check if the player is reloading to prevent ghost reloads.

    private void Awake()
    {
        bulletPool = GameObject.Find("PlayerBulletPooler").GetComponent<Pooler>();
        reloadBar = GameObject.FindGameObjectWithTag("UIManager").GetComponent<ReloadBar>();
    }

    private void Update()
    {
        if (holdOnShoot) { Shoot(); } // Shoot continuously while shoot button is held down.
    }

    private void Shoot() 
    {
        if (!canShoot) { return; }

        if (player.GetCurrentAmmoCount() < 1) // If ammo is depleted and the player attempts to shoot, do an auto reload.
        {
            StartCoroutine(Reload());
        }
        else // Shoot normally
        {
            bulletPool.SpawnObject(barrel.position, barrel.rotation);
            StartCoroutine(StartShootCooldown(player.GetCoolDownBetweenShots()));
        }  
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
        player.DecreaseCurrentAmmoCount();
        player.UpdateAmmoUI();
        canShoot = false;
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }

    public IEnumerator Reload()
    {
        if (isReloading) { yield break; } // in IEnumerator, yield break = return;

        isReloading = true;
        float reloadTime = player.GetReloadTime();
        reloadBar.SetReloadTimer(reloadTime);
        player.EmptyCurrentAmmoCount();
        player.UpdateAmmoUI();
        yield return new WaitForSeconds(reloadTime);
        player.ReloadAmmoCount();
        player.UpdateAmmoUI();
        isReloading = false;
    }
}
