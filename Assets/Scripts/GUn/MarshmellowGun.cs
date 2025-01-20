using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MarshmellowGun : BaseGun
{
    public enum GunState
    {
        Shooting,
        Reloading,
        None
    }

    public GameObject marshBullet;
    public float launchForce = 55f;

    public Transform firePoint;

    public float reloadTime = 2f;
    public float shootDelay = 0.5f;

    public AudioSource reloadSound;
    public AudioSource noAmmoSound;
    public AudioSource ShootSound;





    public TextMeshPro ammoText;
    public GameObject muzzleFlash;

    public Animator glAnim;

    public GameObject icon;

    private bool isReloading = false; // Flag to prevent shooting while reloading
    private bool isShooting = false;  // Flag to prevent rapid firing

    private GameObject marshmellowGun;
    private Transform fartGunTransform;

    private GunState state = GunState.None;

    private void Start()
    {


        glAnim.keepAnimatorStateOnDisable = true;

        muzzleFlash.SetActive(false);
        Ammo = MaxAmmo;
        UpdateAmmoText();

        // Cache the fartGun reference
        marshmellowGun = GameObject.Find("MG-Pos");
        if (marshmellowGun != null)
        {
            fartGunTransform = marshmellowGun.transform.Find("MarshmellowGun");
        }
    }

    public override void Shoot()
    {
        if (isReloading || isShooting || Ammo <= 0) return;

        // If the fire button is held down, continue firing
        if (Input.GetButton("Fire1"))
        {
            // Start the shooting sound if it's not already playing
            if (!ShootSound.isPlaying)
            {
                ShootSound.Play();
            }

            // Only shoot if the delay has passed (avoiding rapid fire)
            if (!isShooting)
            {
                StartCoroutine(ShootWithDelay());
            }

            Vector3 spawnPosition = firePoint.position;

            // Instantiate a marshmallow prefab at the fire point position
            GameObject marshmellow = Instantiate(marshBullet, spawnPosition, firePoint.rotation);

            // Get the Rigidbody of the marshmallow
            Rigidbody rb = marshmellow.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply a launch force to the marshmallow in the forward direction of the fire point
                rb.AddForce(firePoint.forward * launchForce, ForceMode.Impulse);
            }

            Ammo--; // Decrease ammo count
            UpdateAmmoText(); // Update ammo display
        }
        else if (ShootSound.isPlaying)
        {
            // Stop the sound when the fire button is released
            ShootSound.Stop();
        }
    }

    public override void Reload(bool instant)
    {
        if (isReloading || Ammo >= MaxAmmo) return; // Avoid reloading if already full

        if (instant)
        {
            reloadSound.Play();
            CompleteReload();
        }
        else
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ShootWithDelay()
    {
        isShooting = true;
        muzzleFlash.SetActive(true);
        // Optionally play the recoil animation
        // glAnim.Play("Recoil");

        yield return new WaitForSeconds(shootDelay);
        muzzleFlash.SetActive(false);
        glAnim.Play("MG-idle");
        isShooting = false;
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        glAnim.Play("MarshReload");
        reloadSound.Play();
        yield return new WaitForSeconds(reloadTime);
        CompleteReload();
    }

    private void CompleteReload()
    {
        isReloading = false;
        glAnim.Play("MG-idle");
        Ammo = MaxAmmo; // Refill ammo to max
        UpdateAmmoText(); // Update ammo text display
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MMB"))
        {
            if (!isReloading && Ammo < MaxAmmo)
            {
                StartCoroutine(ReloadCoroutine());
                // Add ammo while ensuring it does not exceed MaxAmmo
                Ammo = Mathf.Min(Ammo + 100, MaxAmmo);
                Destroy(other.gameObject);
                UpdateAmmoText(); // Update ammo text to reflect new ammo count
            }
        }
    }

    public override void ThrowGun()
    {
        // Implement throwing logic if needed
    }

    private void UpdateAmmoText()
    {
        if (ammoText != null)
        {
            ammoText.text = Ammo.ToString();
        }
    }

    private void Update()
    {
        if (fartGunTransform != null)
        {
            icon.SetActive(fartGunTransform.gameObject.activeSelf);
        }
    }

    public override bool IsBlocking()
    {
        return state != GunState.None;
    }
}
