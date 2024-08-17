using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipShoot : MonoBehaviour
{
    public Transform[] firePoints; // Points from where projectiles are fired
    public GameObject projectilePrefab; // Prefab of the projectile
    public float fireRate = 1f; // Rate of fire in seconds
    public float detectionRadius = 10f; // Radius to detect enemies
    public float rotationSpeed = 180f; // Rotation speed of fire points
    public float projectileForce = 20f; // Force applied to the projectile

    private float nextFireTime;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            AimAndFire();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    void AimAndFire()
    {
        // Find all enemies within a detection radius
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectionRadius);

        // Find the closest enemy
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;
        foreach (Collider col in hitColliders)
        {
            if (col.CompareTag("Purple")) // Check tag for enemy detection
            {
                float distance = Vector3.Distance(transform.position, col.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEnemy = col.transform;
                }
            }
        }

        // If an enemy is found, aim towards it
        if (closestEnemy != null)
        {
            foreach (Transform firePoint in firePoints)
            {
                // Rotate towards the enemy
                Vector3 direction = (closestEnemy.position - firePoint.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                firePoint.rotation = Quaternion.RotateTowards(firePoint.rotation, lookRotation, rotationSpeed * Time.deltaTime);

                // Fire projectile
                GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);

                // Add force to the projectile (if it has a Rigidbody component)
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddForce(direction * projectileForce, ForceMode.Impulse); // Apply force to the projectile
                }
            }
        }
    }
}
