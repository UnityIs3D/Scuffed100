using System.Collections;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public AudioSource hitBadGuySound;
    private bool canPlaySound = true; // To control sound playback frequency
    public float soundCooldown = 0.5f; // Cooldown time between sound plays

    public GameObject swordBloodPosition; // Not used in current code
    public ParticleSystem swordBloodEffect;

    private void Start()
    {
        if (swordBloodEffect != null)
        {
            swordBloodEffect.Stop();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Purple"))
        {
            // Check if swordBloodEffect is not null before using it
            if (swordBloodEffect != null)
            {
                swordBloodEffect.Play();
            }

            HandleHit(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Purple"))
        {
            // Check if swordBloodEffect is not null before using it
            if (swordBloodEffect != null)
            {
                swordBloodEffect.Play();
            }

            HandleHit(other.gameObject);
        }
    }

    private void HandleHit(GameObject hitObject)
    {
        // Check if hitBadGuySound is not null and can play sound
        if (hitBadGuySound != null && canPlaySound)
        {
            hitBadGuySound.Play();
            StartCoroutine(SoundCooldown());
        }

        // Handle enemy hit logic
        BaseEnemy enemy = hitObject.GetComponent<BaseEnemy>();
        if (enemy != null)
        {
            enemy.OnHit();
        }
    }

    private IEnumerator SoundCooldown()
    {
        canPlaySound = false;
        yield return new WaitForSeconds(soundCooldown);
        canPlaySound = true;
    }
}
