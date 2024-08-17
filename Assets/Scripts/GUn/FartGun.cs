using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class FartGun : BaseGun
{
    public enum GunState
    {
        Shooting,
        Reloading,
        None
    }


    public AudioSource fartSound;
    public Material newMaterial;
    public GameObject battery;
    public GameObject poopCollider;
    public GameObject poopToy;
    public GameObject icon;

    public float poopColliderDuration = 2.5f;
    public float actionCooldown = 2f;

    
    private Material originalMaterial;
    private bool isActionAllowed = true;

    private GunState state = GunState.None;

    private void Start()
    {
        poopCollider.SetActive(false);
        Ammo = MaxAmmo; // Initialize bullets

        if (battery != null)
            originalMaterial = battery.GetComponent<Renderer>().material;
        else
            Debug.LogError("Battery is not assigned!");

        DeactivatePoopCollider();
    }

    public override void Shoot()
    {
        if (Input.GetButtonDown("Fire1") && isActionAllowed)
        {
            if (Ammo > 0) // Check if there are bullets left
            {
                poopToy.SetActive(true);
                StartCoroutine(PerformFartAction());
                Ammo--; // Decrease bullet count after firing
            }
            else
            {
                Debug.Log("Out of poop!");
                // Optionally, play an empty clip sound or display a UI message
            }
            if (Ammo <= 0)
            {
                poopToy.SetActive(false);
            }
        }
    }

    private IEnumerator PerformFartAction()
    {
        isActionAllowed = false;

        // Play sound and change light
        StartCoroutine(PlayFartSound());
        StartCoroutine(ChangeLightDuration());

        // Activate poop collider and deactivate after duration
        poopCollider.SetActive(true);
        yield return new WaitForSeconds(poopColliderDuration);
        poopCollider.SetActive(false);

        yield return new WaitForSeconds(actionCooldown);
        isActionAllowed = true;
    }

    private IEnumerator PlayFartSound()
    {
        fartSound.Play();
        yield return new WaitForSeconds(fartSound.clip.length);
    }

    private IEnumerator ChangeLightDuration()
    {
        if (battery != null && newMaterial != null)
        {
            battery.GetComponent<Renderer>().material = newMaterial;
            yield return new WaitForSeconds(poopColliderDuration);
            battery.GetComponent<Renderer>().material = originalMaterial;
        }
    }

    private void DeactivatePoopCollider()
    {
        poopCollider.SetActive(false);
    }

    public override void ThrowGun()
    {
        // Implementation needed
    }

    public override void Reload(bool instant)
    {
        // Refill the magazine
        Ammo = MaxAmmo;
        Debug.Log("Magazine refilled!");
        poopToy.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PoopAmmo"))
        {
            Debug.Log("Reloading PoopAmmo");
            if (Ammo < MaxAmmo)
            {
                // Add ammo while ensuring it does not exceed MaxAmmo
                Ammo = Mathf.Min(Ammo + 2, MaxAmmo);

                Destroy(other.gameObject);
            }
        }
    }

    private void Update()
    {
        GameObject fartGun = GameObject.Find("FG-Pos");

        if (fartGun != null)
        {
            Transform fartGunTransform = fartGun.transform.Find("FartGun");

            if (fartGunTransform != null && !fartGunTransform.gameObject.activeSelf)
            {
                // Do something when fartGunTransform is not active
                icon.SetActive(false);
            }
            else
            {
                icon.SetActive(true);
            }
        }

        if(Ammo > 0)
        {
            poopToy.SetActive(true);
        }

    }

    public override bool IsBlocking()
    {
        return state != GunState.None;
    }

   

   
}
