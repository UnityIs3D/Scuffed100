using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;




public class SimpleFollow : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject player;
    private float timer; // Timer to keep track of time elapsed

    private float minSpeed = 18f; // Minimum speed value
    private float maxSpeed = 22f; // Maximum speed value

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Adjust NavMeshAgent settings initially
        SetRandomSpeed();
        navMeshAgent.stoppingDistance = 0f; // Ensure the agent gets as close as possible
        navMeshAgent.angularSpeed = 360f; // Set to a reasonable angular speed

        if (player != null)
        {
            navMeshAgent.destination = player.transform.position;
        }
        else
        {
            Debug.Log("Hayden is dead");
        }
    }

    void Update()
    {
        // Check if player exists and NavMeshAgent is active and enabled
        if (player != null && navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.destination = player.transform.position;
        }

        // Timer logic to change speed every 6 seconds
        timer += Time.deltaTime;
        if (timer >= 6f)
        {
            SetRandomSpeed();
            timer = 0f; // Reset timer
        }
    }

    void SetRandomSpeed()
    {
        // Generate a random speed between minSpeed and maxSpeed
        float newSpeed = Random.Range(minSpeed, maxSpeed);
        navMeshAgent.speed = newSpeed;
    }
}
