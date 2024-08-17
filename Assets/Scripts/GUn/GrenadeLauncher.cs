using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GrenadeLauncher : BaseGun
{
    public enum GunState
    {
        Shooting,
        Reloading,
        None
    }


    public GameObject grenadePrefab;
    public float launchForce = 55f;

    public Transform firePoint;

    public float reloadTime = 2f;
    public float shootDelay = 0.5f;

    public AudioSource reloadSound;
    public AudioSource noAmmoSound;

    public TextMeshPro ammoText;
    public GameObject muzzleFlash;

    public Animator glAnim;

    public GameObject icon;

    private bool isReloading = false; // Flag to prevent shooting while reloading
    private bool isShooting = false;  // Flag to prevent rapid firing

    private GameObject fartGun;
    private Transform fartGunTransform;

    private GunState state = GunState.None;

    public AudioSource ShootSound;

    private void Start()
    {
        glAnim.keepAnimatorStateOnDisable = true;

        muzzleFlash.SetActive(false);
        Ammo = MaxAmmo;
        UpdateAmmoText();
        

        // Cache the fartGun reference
        fartGun = GameObject.Find("GL-Pos");
        if (fartGun != null)
        {
            fartGunTransform = fartGun.transform.Find("GrenadeLauncher");
        }
    }

    public override void Shoot()
    {
        if (isReloading || isShooting || Ammo <= 0) return;

        if (Input.GetButton("Fire1"))
        {
            ShootSound.Play();
            if (Ammo <= 0 && !noAmmoSound.isPlaying)
            {
                noAmmoSound.Play();
                return; // Exit the method to avoid shooting
            }

            StartCoroutine(ShootWithDelay());

            Vector3 spawnPosition = firePoint.position;

            // Instantiate a grenade prefab at the fire point position
            GameObject grenade = Instantiate(grenadePrefab, spawnPosition, firePoint.rotation);

            // Get the Rigidbody of the grenade
            Rigidbody rb = grenade.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply a launch force to the grenade in the forward direction of the fire point
                rb.AddForce(firePoint.forward * launchForce, ForceMode.Impulse);
            }

            Ammo--; // Decrease ammo count
            UpdateAmmoText();
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
        glAnim.Play("Recoil");
        yield return new WaitForSeconds(shootDelay);
        muzzleFlash.SetActive(false);
        glAnim.Play("Idle");
        isShooting = false;
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        glAnim.Play("ReloadGL");
        Debug.Log("Reloading...");
        reloadSound.Play();
        yield return new WaitForSeconds(reloadTime);
        CompleteReload();
    }

    private void CompleteReload()
    {
        isReloading = false;
        glAnim.Play("Idle");
        Ammo = MaxAmmo; // Refill ammo to max
        UpdateAmmoText(); // Update ammo text display
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GLAmmo"))
        {
            if (!isReloading && Ammo < MaxAmmo)
            {
                StartCoroutine(ReloadCoroutine());
                // Add ammo while ensuring it does not exceed MaxAmmo
                Ammo = Mathf.Min(Ammo + 2, MaxAmmo);
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
