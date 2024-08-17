using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class YmPush : MonoBehaviour
{
    private float minKnockbackForce = 16f;
    private float maxKnockbackForce = 20f;

    private float minUpwardForce = 16f;
    private float maxUpwardForce = 18f;

    

    private void Start()
    {
        

        // Get the Rigidbody component of the current GameObject
        Rigidbody rb = GetComponent<Rigidbody>();

        // Calculate knockback direction based on current rotation (e.g., along the negative z-axis)
        Vector3 knockbackDirection = -transform.forward;

        // Generate random knockback force within specified range
        float randomKnockbackForce = Random.Range(minKnockbackForce, maxKnockbackForce);

        // Generate random upward force within specified range
        float randomUpwardForce = Random.Range(minUpwardForce, maxUpwardForce);

        // Apply knockback force to the player in the calculated direction
        rb.AddForce(knockbackDirection.normalized * randomKnockbackForce, ForceMode.Impulse);

        // Apply upward force
        rb.AddForce(Vector3.up * randomUpwardForce, ForceMode.Impulse);
    }
}



//public class YmPush : MonoBehaviour
//{
//    public float knockbackForce = 22f; // Adjust this value to control the force of knockback
//    public float upwardForce = 22f; // Adjust this value to control the upward force

//    public GameObject paintSplash;

//    private void Start()
//    {
//        paintSplash.SetActive(true);

//        // Get the Rigidbody component of the current GameObject
//        Rigidbody rb = GetComponent<Rigidbody>();

//        // Calculate knockback direction based on current rotation (e.g., along the negative z-axis)

//        Vector3 knockbackDirection = -transform.forward;
//        Vector3 kockUpDirection = Vector3.up * upwardForce;

//        // Apply knockback force to the player in the calculated direction
//        rb.AddForce(knockbackDirection.normalized * knockbackForce, ForceMode.Impulse);
//        rb.AddForce(kockUpDirection.normalized * upwardForce   , ForceMode.Impulse);
//    }
//}
