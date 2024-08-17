using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class KnockBack : MonoBehaviour
{
    public float knockbackForce = 10f; // Adjust this value to control the force of knockback
    public Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Purple"))
        {
            Debug.Log("OW!");
            // Calculate the direction from the enemy to the player
            Vector3 knockbackDirection = transform.position - collision.transform.position;
            knockbackDirection.Normalize();

            // Apply knockback force to the player in the opposite direction
            GetComponent<Rigidbody>().AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }



        if(collision.gameObject.CompareTag("Hulk"))
        {
            
            if (rb != null)
            {
                // Calculate force direction (upward from the collision point)
                Vector3 forceDirection = collision.contacts[0].normal;
                forceDirection.y = Mathf.Abs(forceDirection.y); // Ensure the force is upwards

                // Add force in the calculated direction
                rb.AddForce(forceDirection * forceMagnitude, ForceMode.Impulse);
            }
        }

    }




    public float forceMagnitude = 10f;
    


   


}
