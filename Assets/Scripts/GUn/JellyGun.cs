using System.Collections;
using UnityEngine;
using TMPro;

public class JellyGun : BaseGun
{
    public enum GunState
    {
        Shooting,
        Reloading,
        None
    }

    public Transform firePoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 300f;
    public float reloadTime = 2f;
    public float shootDelay = 0.5f;

    public AudioSource shootSound;
    public AudioSource reloadSound;
    public AudioSource noAmmoSound;

    public Animator jellyGunAnimationController;
    public TextMeshPro ammoText;
    public GameObject paintSplash;

    private GunState state = GunState.None;

    public GameObject icon;

    private void Start()
    {
        Ammo = MaxAmmo;
        UpdateAmmoText();
        jellyGunAnimationController.keepAnimatorStateOnDisable = true;
    }

    public override void Shoot()
    {
        if (state != GunState.None) return;

        if (Ammo > 0)
        {
            HandleShooting();
            state = GunState.Shooting;
            StartCoroutine(ResetStateCoroutine());
        }
        else if (Input.GetButton("Fire1") && !noAmmoSound.isPlaying)
        {
            noAmmoSound.Play();
        }
    }

    private IEnumerator ResetStateCoroutine()
    {
        yield return new WaitForSeconds(shootDelay);
        state = GunState.None;
    }

    private void HandleShooting()
    {
        if (Ammo <= 0)
        {
            Debug.Log("No ammo to shoot.");
            return;
        }

        shootSound.Play();
        jellyGunAnimationController.SetTrigger("Shoot");
        StartCoroutine(PaintSplashDuration());

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * bulletSpeed;
        }

        Ammo--;
        UpdateAmmoText();
    }

    public override void Reload(bool instant)
    {
        if (state == GunState.Reloading || Ammo >= MaxAmmo) return;

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

    private IEnumerator ReloadCoroutine()
    {
        Debug.Log("Reloading...");
        jellyGunAnimationController.SetTrigger("Reload");
        reloadSound.Play();
        state = GunState.Reloading;

        yield return new WaitForSeconds(reloadTime);
        CompleteReload();
    }

    private void CompleteReload()
    {
        Ammo = MaxAmmo;
        UpdateAmmoText();
        state = GunState.None;
        Debug.Log("Reload completed");
    }

    private IEnumerator PaintSplashDuration()
    {
        //paintSplash.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        //paintSplash.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("JellyAmmo"))
        {
            if (state != GunState.Reloading && Ammo < MaxAmmo)
            {
                Ammo = Mathf.Min(Ammo + 15, MaxAmmo);
                jellyGunAnimationController.SetTrigger("Reload");
                UpdateAmmoText();
                reloadSound.Play();
                state = GunState.Reloading;

                
                CompleteReload();
                Destroy(other.gameObject);
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
        if (icon != null)
        {
            GameObject jellyGunIcon = GameObject.Find("JG-Pos");
            if (jellyGunIcon != null)
            {
                Transform jellyGunTransform = jellyGunIcon.transform.Find("JellyGun");
                if (jellyGunTransform != null)
                {
                    // Check if the JellyGun GameObject is active
                    bool isJellyGunActive = jellyGunTransform.gameObject.activeSelf;
                    // Set the icon active based on the JellyGun status
                    icon.SetActive(isJellyGunActive);
                }
                else
                {
                    // JellyGun not found, so turn off the icon
                    icon.SetActive(false);
                }
            }
        }
    }




    private void OnEnable()
    {
        state = GunState.None;
    }

    private void OnDisable()
    {
        jellyGunAnimationController.Play("Idle");
    }

    public override bool IsBlocking()
    {
        return state != GunState.None;
    }
}
