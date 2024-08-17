using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class FacePlayer : MonoBehaviour
{
    private Transform playerTransform; // Reference to the player's transform

    void Start()
    {
        // Assuming your player has a tag "Player", find the player object by tag
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            //Debug.LogError("Player not found! Make sure the player has the tag 'Player'.");
        }
    }

    void Update()
    {
        // Check if playerTransform is assigned
        if (playerTransform != null)
        {
            // Rotate the gameObject to face the player's position
            Vector3 direction = playerTransform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = rotation;
        }
    }



   

}



