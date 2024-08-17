using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BoxerWar : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    public float speed = 0f; // Adjust this value in the Inspector to control speed

    private GameObject[] purpleObjects;
    private GameObject target;
    private float timeSinceLastTargetChange;
    private float switchInterval = 5f; // Time interval to switch target

    public float animationTriggerDistance;
    public Animator daveModel;
    private bool isAnimating = false;

    public GameObject bulletPrefab;
    public Transform shootingPoint;
    public float bulletForce;

    public float minSpeed;
    public float maxSpeed;

    void Start()
    {
        

        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            
            return;
        }

        StartCoroutine(RandomSpeed());
        navMeshAgent.speed = speed;
        navMeshAgent.stoppingDistance = 0f;
        navMeshAgent.angularSpeed = 360f;

        FindClosestPurpleTarget(); // Call the closest target finder initially
    }

    void Update()
    {
        if (target == null || !target.activeSelf)
        {
            FindClosestPurpleTarget(); // Update target if current one is null or inactive
        }

        if (target != null && navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.destination = target.transform.position;

            // Only set destination if we are not already animating
            if (!isAnimating)
            {
                float distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
                if (distanceToTarget <= animationTriggerDistance)
                {
                    StartCoroutine(RandomAnimations(target));
                }
            }
        }

        // Switch target at regular intervals
        if (Time.time - timeSinceLastTargetChange >= switchInterval)
        {
            SwitchToRandomEnemy();
            timeSinceLastTargetChange = Time.time;
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
            navMeshAgent.destination = target != null ? target.transform.position : transform.position;
            timeSinceLastTargetChange = Time.time;
        }
        else
        {
            target = null;
            navMeshAgent.destination = transform.position; // Stop movement if no target is found
        }
    }

    void SwitchToRandomEnemy()
    {
        purpleObjects = GameObject.FindGameObjectsWithTag("Purple");

        if (purpleObjects.Length > 0)
        {
            int randomIndex = Random.Range(0, purpleObjects.Length);
            target = purpleObjects[randomIndex];
            navMeshAgent.destination = target.transform.position;
        }
        else
        {
            target = null;
            navMeshAgent.destination = transform.position; // Stop movement if no target is found
        }
    }

    private IEnumerator RandomSpeed()
    {
        while (true)
        {
            speed = Random.Range(minSpeed, maxSpeed);
            if (navMeshAgent != null)
                navMeshAgent.speed = speed;
            yield return new WaitForSeconds(0.9f);
        }
    }

    private IEnumerator RandomAnimations(GameObject currentTarget)
    {
        isAnimating = true;

        if (daveModel != null)
        {
            int randomAnimation = Random.Range(0, 3); // 0, 1, or 2
            Debug.Log(randomAnimation);

            if (randomAnimation == 0)
            {
                daveModel.Play("Punch");
                yield return new WaitForSeconds(1.5f); // Adjust this time according to your animation length
            }
            else if (randomAnimation == 1)
            {
                daveModel.Play("Stomp");
                yield return new WaitForSeconds(1.5f); // Adjust this time according to your animation length
            }
            else if (randomAnimation == 2)
            {
                yield return StartCoroutine(ShootBullet()); // Wait for shooting animation to complete
            }
        }
       

        isAnimating = false;
    }

    private IEnumerator ShootBullet()
    {
        if (daveModel != null && bulletPrefab != null && shootingPoint != null)
        {
            daveModel.Play("Throw");

            yield return new WaitForSeconds(1.3f); // Adjust this time according to your shooting animation length

            GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();

            if (bulletRigidbody != null)
            {
                Vector3 forceDirection = shootingPoint.forward;
                bulletRigidbody.AddForce(forceDirection * bulletForce, ForceMode.Impulse);
            }

        }
    }
}