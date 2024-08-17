using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Jerry : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public float speed = 0f; // Adjust this value in the Inspector to control speed

    private GameObject[] purpleObjects;
    private GameObject target;
    private float timeSinceLastTargetChange;
    public float switchInterval = 0f; // Time interval to switch target

    public float animationTriggerDistance;
    public Animator rollAttack;
    public Animator jerryRunEat;
    public GameObject orientation;
    public GameObject jerryHimself;
    public GameObject yellowBlood;

    // Health script
    public int maxHealth = 200;
    public int currentHealth;
    public int takeDamage = 10;

    void Start()
    {
        this.gameObject.name = "RockMan";

        currentHealth = maxHealth; // Initialize health

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;
        navMeshAgent.stoppingDistance = 0f;
        navMeshAgent.angularSpeed = 360f;

        StartCoroutine(RollActive());
        StartCoroutine(RandomSpeed());
        StartCoroutine(RandomSecSwtichRandomTarget());

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
                jerryRunEat.Play("Eat");
                yellowBlood.SetActive(true);
            }
            else
            {
                yellowBlood.SetActive(false);
            }
        }
    }

    void FindClosestPurpleTarget()
    {
        purpleObjects = GameObject.FindGameObjectsWithTag("Purple");

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
            timeSinceLastTargetChange = Time.time; // Set the time when target is found
        }
        else
        {
            target = null;
        }
    }

    void SwitchToRandomEnemy()
    {
        purpleObjects = GameObject.FindGameObjectsWithTag("Purple");

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
            speed = Random.Range(22f, 24f);
            if (navMeshAgent != null) // Update speed for navMeshAgent if it's not null
            {
                navMeshAgent.speed = speed;
            }
            yield return new WaitForSeconds(2f);
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

    private IEnumerator RollActive()
    {
        while (true)
        {
            float rollWhenever = Random.Range(4f, 13f);

            yield return new WaitForSeconds(rollWhenever);
            rollAttack.enabled = true;

            yield return new WaitForSeconds(rollWhenever);
            rollAttack.enabled = false;

            if (!rollAttack.enabled)
            {
                jerryHimself.transform.rotation = Quaternion.Euler(0, 0, 0);
                orientation.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Purple"))
        {
            if (jerryRunEat.GetCurrentAnimatorStateInfo(0).IsName("Eat") || rollAttack.enabled)
            {
                BaseEnemy enemy = other.gameObject.GetComponent<BaseEnemy>();
                if (enemy != null)
                {
                    enemy.OnHit();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Purple"))
        {
            TakeDamage(takeDamage);
        }
    }

    private void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    
}
