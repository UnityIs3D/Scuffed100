using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomDrop : MonoBehaviour
{
    public GameObject[] whatever; // Array of GameObjects to choose from
    public float dropChance = 0.33f; // Chance to drop an item

    private void Start()
    {
        // Check if the array is not empty to avoid errors
        if (whatever.Length == 0)
        {
            
            return;
        }

        // Generate a random number between 0 and 1
        float randomDropChance = Random.Range(0f, 1f);

        // Check if the generated number is less than or equal to dropChance
        if (randomDropChance <= dropChance)
        {
            // Randomly select one of the GameObjects from the array
            GameObject itemToSpawn = whatever[Random.Range(0, whatever.Length)];

            // Instantiate the selected GameObject at the current position and rotation
            Instantiate(itemToSpawn, transform.position, transform.rotation);
        }
    }
}
