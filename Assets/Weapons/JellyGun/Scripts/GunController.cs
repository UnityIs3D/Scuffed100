using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;




public class GunController : MonoBehaviour
{
    public Transform firePoint;         // The point where bullets are spawned
    public GameObject bulletPrefab;     // Prefab of the bullet object
    public float bulletSpeed = 300f;    // Speed of the bullet
    public int maxAmmo = 10;            // Maximum ammo capacity
    public float reloadTime = 2f;       // Time it takes to reload
    public float shootDelay = 0.5f;     // Delay before shooting again

    public int currentAmmo;             // Current ammo count
    private bool isReloading = false;   // Flag to prevent shooting while reloading
    private bool isShooting = false;    // Flag to prevent rapid firing

    public AudioSource shootSound; // jellySound
    public AudioSource noAmmoSound; // noAmmoSound
    public AudioSource reloadSound; // reloadSound

    public Animator JellyGunAnimationController;

    public TextMeshPro ammoText;

    public GameObject paintSplash;

    void Start()
    { 
        currentAmmo = maxAmmo;  // Initialize current ammo to max ammo 
    }

    void Update()
    {
        if (isReloading || isShooting)
            return;

        // Check for input to shoot (you can modify this as needed)
        if (Input.GetButtonDown("Fire1") && currentAmmo > 0)
        {
            StartCoroutine(ShootWithDelay());
            shootSound.Play();
            StartCoroutine(startPew());

            StartCoroutine(PaintSplashDuration());
            
        }

        // Check for input to reload
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
        {
            StartCoroutine(Reload());
            reloadSound.Play();
        }

        else if (Input.GetMouseButtonDown(0) && currentAmmo <= 0)
        {
            Debug.Log("Can't Shoot: No Ammo");
            noAmmoSound.Play();

        }

        ammoText.text = "" + currentAmmo;// ammo display Text Counter
    }

    IEnumerator ShootWithDelay()
    {
        isShooting = true;
        Shoot();  // Perform initial shoot

        yield return new WaitForSeconds(shootDelay);  // Wait for delay before allowing next shot
        isShooting = false;  // Reset shooting flag
    }

    void Shoot()
    {
        // Spawn a bullet at the firePoint's position and rotation
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Optionally, add velocity to the bullet if it has a Rigidbody component
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.up * bulletSpeed; // Set the velocity of the bullet
        }

        currentAmmo--;  // Decrease ammo count
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");
        JellyGunAnimationController.GetComponent<Animator>().Play("Reload");
        yield return new WaitForSeconds(reloadTime);  // Simulate reload time
        JellyGunAnimationController.GetComponent<Animator>().Play("Nothing");
        currentAmmo = maxAmmo;  // Refill ammo
        isReloading = false;
    }


    IEnumerator startPew()
    {
        JellyGunAnimationController.GetComponent<Animator>().Play("Shooting");
        yield return new WaitForSeconds(0.20f);
        JellyGunAnimationController.GetComponent<Animator>().Play("Nothing");
    }





    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("JellyAmmo"))
        {
            reloadSound.Play();
            StartCoroutine(Reload());
            Debug.Log("JellyAmmo obtained");
            Destroy(other.gameObject);
        }
    }


    IEnumerator PaintSplashDuration()
    {
        paintSplash.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        paintSplash.SetActive(false);
    }


}
