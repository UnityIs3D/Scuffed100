using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MarshPush : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Rigidbody rb;
    public float knockBackForce = 100f;
    public float upWardForce = 12f;

    private bool hasTouchedMarsh = false;  // Flag to track if Marsh has been touched

    private void Start()
    {
        // Optionally get the components if not assigned via inspector
        if (navMeshAgent == null) navMeshAgent = GetComponent<NavMeshAgent>();
        if (rb == null) rb = GetComponent<Rigidbody>();
    }

    // Detect the first touch with the Marsh
    private void OnCollisionEnter(Collision other)
    {
        // Check if the object touched the Marsh
        if (other.gameObject.CompareTag("Marsh"))
        {
           
            if (navMeshAgent != null)
            {
                navMeshAgent.enabled = false;
            }
            // Apply forces for knockback and upward force
            if (rb != null)
            {
                Vector3 backwardForce = -transform.forward * knockBackForce;  // Knockback force
                Vector3 upwardForce = transform.up * upWardForce;  // Upward force
                rb.AddForce(backwardForce + upwardForce, ForceMode.Impulse);
            }
            rb.constraints = RigidbodyConstraints.None;  // Allow free movement of the Rigidbody
        }
    }

    public GameObject deadBody;

    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.CompareTag("Marsh"))
        {
            hasTouchedMarsh = true;  
        }

        else if (hasTouchedMarsh)
        {
            StartCoroutine(DelayDeath());
        }

        Destroy(gameObject, 20);
    }


    private IEnumerator DelayDeath()
    {
        yield return new WaitForSeconds(16);
        Instantiate(deadBody, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
