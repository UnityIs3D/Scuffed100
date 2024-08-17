using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Trampoline : MonoBehaviour
{
    public float bounceForce = 10f; // Adjust this value to control the bounce force

    private void OnCollisionEnter(Collision collision)
    {
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();

        if (rb != null)
        {
            Vector3 bounceDirection = transform.up; // Bounce in the direction of the trampoline's up vector
            rb.AddForce(bounceDirection * bounceForce, ForceMode.Impulse);
        }
    }
}
