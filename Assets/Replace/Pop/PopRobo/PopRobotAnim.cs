using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using TMPro;

public class PopRobotAnim : MonoBehaviour
{
    public GameObject fireballPrefab; // Reference to the fireball prefab
    public Transform fireballSpawnPoint; // Reference to where the fireball should be spawned

    private GameObject hayden; // Reference to the player GameObject
    public Animator animator;

    public float attackRange = 5f;
    public float actionInterval = 2f; // Interval between actions

    private FollowManyPlayers followPlayerScript;

    private ScoreManager scoreManager;

    private void Start()
    {
        this.gameObject.name = "PopRobot";
        scoreManager = FindObjectOfType<ScoreManager>();

        explosionEffect.Stop();
        hayden = GameObject.FindGameObjectWithTag("Player");
        InvokeRepeating("ThrowFireBall", 0, 8f); // Corrected InvokeRepeating usage
        followPlayerScript = GetComponent<FollowManyPlayers>();
    }

    private void Update()
    {
        // Check if player GameObject is assigned
        if (hayden != null)
        {
            // Check if the player GameObject is inactive
            if (!hayden.activeSelf)
            {
                if (followPlayerScript != null)
                {
                    followPlayerScript.enabled = false;
                }

                // Play "SambaDance" animation
                if (animator != null)
                {
                    animator.Play("Flair");
                    followPlayerScript.enabled = false;
                }

            }
        }

        // Example check for kicking
        Kick();
    }

    private void Kick()
    {
        if (hayden != null && Vector3.Distance(transform.position, hayden.transform.position) <= attackRange)
        {
            if (animator != null)
            {
                animator.SetTrigger("Kick");
            }
        }
    }

    private void ThrowFireBall()
    {
        if (fireballPrefab != null && fireballSpawnPoint != null)
        {
            if (animator != null)
            {
                animator.SetTrigger("Throw");
            }
            StartCoroutine(fireBallThrowDelay());
        }
    }

    private IEnumerator fireBallThrowDelay()
    {
        yield return new WaitForSeconds(0.7f);
        Instantiate(fireballPrefab, fireballSpawnPoint.position, fireballSpawnPoint.rotation);
    }

    public ParticleSystem explosionEffect;

    


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
