using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro; 

public class HornAnim : MonoBehaviour
{
    private GameObject player; // Reference to the player GameObject
    public float attackRange = 5f; // Distance threshold for triggering attack

    public Animator animator; // Animator component for handling animations

    private NavMeshAgent enemyNavMesh;

    public ParticleSystem explosionEffect;

    private ScoreManager scoreManager; // Reference to the ScoreManager

    private void Start()
    {
        this.gameObject.name = "Pop-2";

        explosionEffect.Stop();

        player = GameObject.FindGameObjectWithTag("Player");

        enemyNavMesh = GetComponent<NavMeshAgent>();

        scoreManager = FindObjectOfType<ScoreManager>();

    }

    void Update()
    {
        // Check if player GameObject is assigned
        if (player != null)
        {
            // Check if the player GameObject is inactive
            if (!player.activeSelf)
            {
                // Stop playing "HornSword" and start playing "SambaDance"
                animator.Play("SambaDance");
                Debug.Log("Enemy Dancing");
                enemyNavMesh.enabled = false;
            }
            else
            {
                // Check if within attack range to play "HornSword" animation
                if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
                {
                    // Play "HornSword" animation
                    animator.Play("HornSword");
                    
                }
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("bullet"))
        {
            explosionEffect.Play();
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

}
