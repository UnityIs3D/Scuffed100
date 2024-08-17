using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EagleMinion : MonoBehaviour
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


    public float minSpeed;
    public float maxSpeed;


    void Start()
    {
        StartCoroutine(RandomSpeed());
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
            if (distanceToTarget <= animationTriggerDistance && !isAnimating)
            {
                StartCoroutine(RandomAnimations(target));
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
            timeSinceLastTargetChange = Time.time;
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
            speed = Random.Range(minSpeed, maxSpeed);
            yield return new WaitForSeconds(0.9f);
        }
    }

    private IEnumerator RandomAnimations(GameObject currentTarget)
    {
        isAnimating = true;

        int randomAnimation = Random.Range(0, 2); // 0, 1, or 2
        Debug.Log(randomAnimation);

        if (randomAnimation == 0)
        {
            daveModel.SetTrigger("stab");
            yield return new WaitForSeconds(1.5f); // Adjust this time according to your animation length
        }
        else if (randomAnimation == 1)
        {
            daveModel.SetTrigger("swing");
            yield return new WaitForSeconds(1.5f); // Adjust this time according to your animation length
        }
        
        isAnimating = false;
    }

   
    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Purple"))
    //    {

    //        if (daveModel.GetCurrentAnimatorStateInfo(0).IsName("stab") || daveModel.GetCurrentAnimatorStateInfo(0).IsName("swing") || daveModel.GetCurrentAnimatorStateInfo(0).IsName("Stomp"))
    //        {
    //            BaseEnemy enemy = other.gameObject.GetComponent<BaseEnemy>();
    //            if (enemy != null)
    //            {
    //                enemy.OnHit();
    //            }
    //        }


    //    }
    //}


}
