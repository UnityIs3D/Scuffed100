using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FollowManyPlayers : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private List<GameObject> players;  // List of all Player GameObjects with "Player" tag
    private GameObject currentTarget;  // Currently followed player
    private float switchTimer;  // Timer to switch to another player
    public float followTimeLimit = 0f;  // Time limit to follow one player

    public float minSpeed = 17f; // Minimum speed value
    public float maxSpeed = 20f; // Maximum speed value

    void Start()
    {
        StartCoroutine(SetRandomSpeed());
        StartCoroutine(RandomSecSwtichRandomTarget());

        players = new List<GameObject>();

        // Find all GameObjects with the tag "Player"
        GameObject[] playerObjects = GameObject.FindGameObjectsWithTag("Player"); 
        players.AddRange(playerObjects);

        navMeshAgent = GetComponent<NavMeshAgent>();

        // Adjust NavMeshAgent settings initially
        SetRandomSpeed();
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
                navMeshAgent.destination = currentTarget.transform.position;
            }

            // Check if it's time to switch to another player
            switchTimer += Time.deltaTime;
            if (switchTimer >= followTimeLimit)
            {
                SwitchToNextPlayer();
                switchTimer = 0f; // Reset timer
            }
        }

        // Timer logic to change speed every 6 seconds (optional)
        
    }

    

    private IEnumerator SetRandomSpeed()
    {
        while (true)
        {
            float newSpeed = Random.Range(minSpeed, maxSpeed);
            yield return new WaitForSeconds(1f);
            navMeshAgent.speed = newSpeed;
        }
        
    }

    void SwitchToNextPlayer()
    {
        // Choose a random player from the list
        if (players.Count > 0)
        {
            int randomIndex = Random.Range(0, players.Count);
            currentTarget = players[randomIndex];
            switchTimer = 0f; // Reset the timer when switching to a new player
        }
        else
        {
            currentTarget = null;
            Debug.Log("No players");
        }
    }


    private IEnumerator RandomSecSwtichRandomTarget()
    {
        while (true)
        {
            followTimeLimit = Random.Range(6f, 13f);
            yield return new WaitForSeconds(0f);

        }
    }

}
