using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSkeleton : MonoBehaviour
{
    private float minKnockbackForce = 22f;
    private float maxKnockbackForce = 20f;

    public float minUpwardForce = 33f;
    public float maxUpwardForce = 35f;

    public Animator skeltonModel;

    private int randomFall;

    private void Start()
    {

        randomFall = Random.Range(0, 2);

        if(randomFall == 1)
        {
            skeltonModel.Play("FallOver");
        }
        else
        {
            skeltonModel.Play("SweepFall");
        }

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

        Destroy(gameObject, 6);
    }

    
}
