using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HornSwordLevel2 : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public float speed = 0f; // Adjust this value in the Inspector to control speed

    private GameObject[] purpleObjects;
    private GameObject target;
    private float timeSinceLastTargetChange;
    public float switchInterval = 0f; // Time interval to switch target

    public float animationTriggerDistance;
    public Animator stuartKnifeModel;

    public int minSpeed = 20;
    public int maxSpeed = 25;



    void Start()
    {
        StartCoroutine(RandomSpeed());
        StartCoroutine(RandomSecSwtichRandomTarget());

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        navMeshAgent.stoppingDistance = 0f;
        navMeshAgent.angularSpeed = 360f;



        FindClosestPurpleTarget(); // Call the closest target finder initially
    }

    void Update()
    {
        if (target == null || !target.activeSelf)
        {
            FindClosestPurpleTarget(); // Update target if current one is null, inactive, or time elapsed
        }

        if (target != null && navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.destination = target.transform.position;
        }

        if (Time.time - timeSinceLastTargetChange >= switchInterval)
        {
            SwitchToRandomEnemy();
            timeSinceLastTargetChange = Time.time; // Reset timer correctly to Time.time
        }

        // Check if navMeshAgent is active and enabled before using it
        if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled && target != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            if (distanceToTarget <= animationTriggerDistance)
            {
                stuartKnifeModel.Play("HornSword");
            }
            else
            {
                //Run
            }
        }
    }

    void FindClosestPurpleTarget()
    {
        purpleObjects = GameObject.FindGameObjectsWithTag("Player");

        if (purpleObjects.Length > 0)
        {
            GameObject closestTarget = null;
            float closestDistance = Mathf.Infinity;
            Vector3 currentPosition = transform.position;

            foreach (GameObject purple in purpleObjects)
            {
                if (purple.activeSelf)
                {
                    float distance = Vector3.Distance(currentPosition, purple.transform.position);
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestTarget = purple;
                    }
                }
            }

            target = closestTarget;
            timeSinceLastTargetChange = Time.deltaTime;
        }
        else
        {
            target = null;
        }
    }

    void SwitchToRandomEnemy()
    {
        purpleObjects = GameObject.FindGameObjectsWithTag("Player");

        if (purpleObjects.Length > 0)
        {
            int randomIndex = Random.Range(0, purpleObjects.Length);
            target = purpleObjects[randomIndex];
        }
        else
        {
            target = null;
        }
    }

    private IEnumerator RandomSpeed()
    {
        while (true)
        {
            speed = Random.Range(minSpeed, maxSpeed);
            yield return new WaitForSeconds(0.9f);
        }
    }




    private IEnumerator RandomSecSwtichRandomTarget()
    {
        while (true)
        {
            switchInterval = Random.Range(6f, 16f);
            yield return new WaitForSeconds(0.2f);

        }
    }
}
