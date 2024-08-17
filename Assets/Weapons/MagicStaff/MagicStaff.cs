using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStaff : BaseGun
{
    public enum GunState
    {
        Shooting,
        Reloading,
        None
    }

    private GunState state = GunState.None;

    public GameObject magicBullet; // Assign this in the Inspector
    public Transform firePoint; // The point from where the projectile will be fired
    public float bulletSpeed = 20f; // Speed of the projectile

    private bool isReloading = false;

    public int reloadTime = 7;
    public GameObject TopStone;
    public GameObject bottomStone;

    public GameObject icon;

    private void Start()
    {
        Ammo = MaxAmmo;
        UpdateStoneStatus(); // Ensure stones are correctly set at the start
    }

    private void Update()
    {
        // Update stone status based on current ammo
        UpdateStoneStatus();

        GameObject magicIcon = GameObject.Find("Magic-Pos");

        if (magicIcon != null)
        {
            Transform magicTransform = magicIcon.transform.Find("Magic");

            if (magicTransform != null && !magicTransform.gameObject.activeSelf)
            {

                icon.SetActive(false);
            }
            else
            {
                icon.SetActive(true);
            }
        }
    }

    private void UpdateStoneStatus()
    {
        if (Ammo <= 0)
        {
            TopStone.SetActive(false);
            bottomStone.SetActive(false);
        }
        else
        {
            TopStone.SetActive(true);
            bottomStone.SetActive(true);
        }
    }

    public override void ThrowGun()
    {
        //throw new System.NotImplementedException();
    }

    public override void Reload(bool instant)
    {
        if (isReloading || Ammo >= MaxAmmo) return; // Avoid reloading if already full

        if (instant)
        {
            CompleteReload();
        }
        else
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private void CompleteReload()
    {
        isReloading = false;
        Ammo = MaxAmmo; // Refill ammo to max
        UpdateStoneStatus(); // Ensure stones are updated after reloading
    }

    public override void Shoot()
    {
        if (Input.GetButtonDown("Fire1")) // Check if Fire1 is pressed
        {
            if (Ammo > 0)
            {
                if (magicBullet && firePoint)
                {
                    // Instantiate the projectile
                    GameObject projectile = Instantiate(magicBullet, firePoint.position, firePoint.rotation);

                    // Get the Rigidbody component and set its velocity
                    Rigidbody rb = projectile.GetComponent<Rigidbody>();
                    if (rb)
                    {
                        rb.velocity = firePoint.forward * bulletSpeed;
                    }

                    Ammo--; // Decrement ammo count
                    UpdateStoneStatus(); // Ensure stones are updated after shooting
                }
            }
            else
            {
                if (!isReloading)
                {
                    StartCoroutine(ReloadCoroutine());
                }
            }
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true; // Set reloading flag
        yield return new WaitForSeconds(reloadTime); // Wait for specified reload time
        CompleteReload(); // Refill ammo
    }

    public override bool IsBlocking()
    {
        return state != GunState.None;
    }
}
