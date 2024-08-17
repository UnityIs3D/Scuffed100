using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeBullet : MonoBehaviour
{
    public float explosionForce = 1000f;  // Force of the explosion upwards
    public float explosionRadius = 5f;    // Radius of the explosion effect

    private bool exploded = false;  // Flag to track if the bullet has exploded

    public GameObject explosionEffect;

    public GameObject skeletonsDead;

    private MeshRenderer skingBullet;

    public AudioSource GlExplosionSound;

    private void Start()
    {
        skingBullet = GetComponent<MeshRenderer>();

        explosionEffect.SetActive(false);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!exploded)
        {
            // Mark as exploded to prevent multiple explosions
            exploded = true;

            // Play the explosion sound
            if (GlExplosionSound != null)
            {
                GlExplosionSound.Play();
            }

            skingBullet.enabled = false;

            // Apply explosion force
            ApplyExplosionForce(transform.position);

            // Destroy objects with the tag "Purple" within the explosion radius
            Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject.CompareTag("Purple"))
                {
                    Instantiate(skeletonsDead, collider.transform.position, collider.transform.rotation);
                    Destroy(collider.gameObject);
                }
            }

            // Activate explosion effect
            if (explosionEffect != null)
            {
                explosionEffect.SetActive(true);
            }

            // Destroy the grenade object after 2 seconds
            Destroy(gameObject, 2f);
        }
    }

    void ApplyExplosionForce(Vector3 explosionPosition)
    {
        // Get all colliders within the explosion radius
        Collider[] colliders = Physics.OverlapSphere(explosionPosition, explosionRadius);

        foreach (Collider hit in colliders)
        {
            // Get the Rigidbody of the object
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                // Calculate direction from explosion position to object
                Vector3 direction = hit.transform.position - explosionPosition;

                // Apply explosion force
                rb.AddForce(direction.normalized * explosionForce, ForceMode.Impulse);
            }
        }
    }
}
