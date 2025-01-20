using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DistanceBody : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private List<Transform> players; // List of all Player Transforms with "Player" tag  
    private Transform currentTarget; // Currently followed player  
    private float switchTimer; // Timer to switch to another player  
    private float followTimeLimit = 0f; // Time limit to follow one player  
    public float minSpeed = 17f; // Minimum speed value  
    public float maxSpeed = 20f; // Maximum speed value  

    // Separation variables  
    public float separationRadius = 5f; // Radius for checking proximity  
    public float minimumSeparationDistance = 2f; // Minimum distance to maintain between agents  
    public float personalSpaceRadius = 3f; // Radius for personal space  

    void Start()
    {
        players = new List<Transform>(); // Find all Transforms with the tag "Player"  
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in playerObjects)
        {
            players.Add(player.transform);
        }

        navMeshAgent = GetComponent<NavMeshAgent>(); // Adjust NavMeshAgent settings initially  
        navMeshAgent.stoppingDistance = 0f; // Ensure the agent gets as close as possible  
        navMeshAgent.angularSpeed = 360f; // Set to a reasonable angular speed  

        // Start following a random player from the list  
        SwitchToNextPlayer();
    }

    void Update()
    {
        if (currentTarget == null)
        {
            SwitchToNextPlayer();
        }
        else
        {
            // Check if the NavMeshAgent is active and enabled  
            if (navMeshAgent.isActiveAndEnabled)
            {
                // Set the destination to the current target  
                navMeshAgent.SetDestination(currentTarget.position);
            }

            // Check if it's time to switch to another player  
            switchTimer += Time.deltaTime;
            if (switchTimer >= followTimeLimit)
            {
                StartCoroutine(SwitchToNextPlayerWithDelay());
            }

            // Apply separation logic  
            ApplySeparationLogic();
        }
    }

    IEnumerator SwitchToNextPlayerWithDelay()
    {
        SwitchToNextPlayer();
        switchTimer = 0f; // Reset timer  
        followTimeLimit = Random.Range(6f, 13f); // Randomize follow time limit again  
        SetRandomSpeed(); // Set a new random speed  

        yield return null;
    }

    void SetRandomSpeed()
    {
        float newSpeed = Random.Range(minSpeed, maxSpeed);
        navMeshAgent.speed = newSpeed;
    }

    void SwitchToNextPlayer()
    {
        // Choose a random player from the list  
        if (players.Count > 0)
        {
            int randomIndex = Random.Range(0, players.Count);
            currentTarget = players[randomIndex];
        }
        else
        {
            currentTarget = null;
            Debug.Log("No players");
        }
    }

    void ApplySeparationLogic()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, separationRadius);
        foreach (var collider in colliders)
        {
            if (collider.gameObject != gameObject)
            {
                Vector3 direction = transform.position - collider.transform.position;
                float distance = direction.magnitude;
                if (distance < minimumSeparationDistance)
                {
                    // Move this agent away from the other  
                    Vector3 moveDirection = direction.normalized * (minimumSeparationDistance - distance);
                    transform.position += moveDirection * Time.deltaTime; // Adjusted to frame-rate independent movement  
                }
            }
        }

        // Check for personal space  
        Collider[] personalSpaceColliders = Physics.OverlapSphere(transform.position, personalSpaceRadius);
        foreach (var collider in personalSpaceColliders)
        {
            if (collider.gameObject != gameObject)
            {
                Vector3 direction = transform.position - collider.transform.position;
                float distance = direction.magnitude;
                if (distance < personalSpaceRadius)
                {
                    // Move this agent away from the other  
                    Vector3 moveDirection = direction.normalized * (personalSpaceRadius - distance);
                    transform.position += moveDirection * Time.deltaTime; // Adjusted to frame-rate independent movement  
                }
            }
        }
    }
}
