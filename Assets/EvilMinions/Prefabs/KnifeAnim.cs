        using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro; // Ensure you have the correct namespace for TextMeshPro

public class KnifeAnim : MonoBehaviour
{
    private GameObject player; // Reference to the player GameObject
    public float attackRange = 5f; // Distance threshold for triggering attack

    public Animator animator; // Animator component for handling animations

    private NavMeshAgent enemyNavMesh;

    public ParticleSystem explosionEffect;

    private ScoreManager scoreManager; // Reference to the ScoreManager

    private void Start()
    {
        this.gameObject.name = "Pop-1";

        explosionEffect.Stop();
        player = GameObject.FindGameObjectWithTag("Player");

        enemyNavMesh = GetComponent<NavMeshAgent>();

        // Find the ScoreManager in the scene
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
                    // Play "attack" animation
                    animator.Play("KnifeAttack");
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
