using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HulkJump : MonoBehaviour
{
    public float jumpForce = 10f;        // Force with which the enemy jumps
    public float jumpInterval = 2f;      // Time interval between jumps
    public float jumpHeight = 3f;        // Height the enemy will jump to

    private Rigidbody rb;
    private float lastJumpTime;          // Time when the enemy last jumped

    public GameObject eMja;

    private ScoreManager scoreManager;

    void Start()
    {
        this.gameObject.name = "PopJumper";
        scoreManager = FindObjectOfType<ScoreManager>();

        rb = GetComponent<Rigidbody>();
        lastJumpTime = -jumpInterval; // Set initially to allow immediate jump
    }

    void Update()
    {
        eMja.GetComponent<Animator>().Play("JumpAttack");
        // Check if enough time has passed since the last jump
        if (Time.time - lastJumpTime > jumpInterval)
        {
            JumpTowardsRandomPlayer();
            lastJumpTime = Time.time; // Update last jump time
        }
    }

    void JumpTowardsRandomPlayer()
    {
        // Find all GameObjects with the "Player" tag
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        if (players.Length > 0)
        {
            // Choose a random player
            GameObject randomPlayer = players[Random.Range(0, players.Length)];
            if (randomPlayer != null)
            {
                Vector3 direction = (randomPlayer.transform.position - transform.position).normalized;

                // Calculate jump direction (horizontal) and vertical jump force
                Vector3 jumpDirection = new Vector3(direction.x, 0f, direction.z);
                rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
                rb.AddForce(Vector3.up * Mathf.Sqrt(2f * jumpHeight * Mathf.Abs(Physics.gravity.y)), ForceMode.VelocityChange);
            }
        }
    }

    private void OnDestroy()
    {
        // This method is called when the GameObject is destroyed
        if (scoreManager != null)
        {
            scoreManager.HandleObjectDestroyed(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Marsh"))
        {
            // Disable the NavMeshAgent to stop pathfinding
            

            if (rb != null)
            {
                // Get the backward direction (opposite of the forward vector)
                Vector3 backwardForce = -transform.forward * 30;  // Adjust for the backward force strength

                // Get the upward direction (along the Y-axis)
                Vector3 upwardForce = transform.up * 30;  // Adjust for the upward force strength

                // Apply both backward and upward forces
                rb.AddForce(backwardForce + upwardForce, ForceMode.Impulse);
            }

            
        }
    }
}

