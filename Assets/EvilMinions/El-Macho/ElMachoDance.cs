using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class ElMachoDance : MonoBehaviour
{

    private Animator animator;
    private GameObject player;
    private NavMeshAgent enemyNavMesh;

    public ParticleSystem explosionEffect;

    private ElMacho elMachoScript;

    private ScoreManager scoreManager;

    private void Start()
    {
        this.gameObject.name = "ElMacho";
        scoreManager = FindObjectOfType<ScoreManager>();

        explosionEffect.Stop();

        player = GameObject.FindGameObjectWithTag("Player");

        enemyNavMesh = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        elMachoScript = GetComponent<ElMacho>();
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
                elMachoScript.enabled = false;
                animator.Play("ChickenDance");
                Debug.Log("Enemy Dancing");
                enemyNavMesh.enabled = false;
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
