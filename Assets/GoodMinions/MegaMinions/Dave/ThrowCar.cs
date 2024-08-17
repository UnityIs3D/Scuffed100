
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowCar : MonoBehaviour
{
    public GameObject floorExplosionPrefab; // Use a prefab for the explosion

    private bool hasCollidedWithGround = false; // Track if the collision with the ground has occurred

    // Update is called once per frame
    void Update()
    {
        // Optionally destroy the object after a certain time
        Destroy(gameObject, 5f);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Purple"))
        {
            //BaseEnemy enemy = other.gameObject.GetComponent<BaseEnemy>();
            //if (enemy != null)
            //{
            //    enemy.OnHit();
            //}
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            if (!hasCollidedWithGround) // Only trigger the explosion once
            {
                hasCollidedWithGround = true; // Prevent multiple triggers
                // Instantiate the explosion effect
                if (floorExplosionPrefab != null)
                {
                    Instantiate(floorExplosionPrefab, transform.position, Quaternion.identity);
                }
                
            }
        }
    }
}
