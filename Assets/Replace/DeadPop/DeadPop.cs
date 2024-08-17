using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPop : MonoBehaviour
{
    public GameObject whatever;
    public float dropChance = 0.33f;

    private void Start()
    {
        float randomDropChance = Random.Range(0f, 1f);

        // Check if the generated number is less than or equal to 0.33
        if (randomDropChance <= dropChance)
        {
            // Spawn the poopAmmo
            Instantiate(whatever, transform.position, transform.rotation);
        }
    }
}


