using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieBullet : MonoBehaviour
{

    

    private void OnCollisionEnter(Collision other)
    {
        // Check if the other collider's GameObject is on the "Ground" layer
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            // Destroy the current GameObject
            Destroy(gameObject);
        }
    }
}
