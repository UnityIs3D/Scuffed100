using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class BadBoxer : MonoBehaviour
{
    [Header("Configuration")]
    public float speed = 0f;
    public float animationTriggerDistance;
    public Animator animModel;
    public GameObject bulletPrefab;
    public Transform shootingPoint;
    public float bulletForce;

    private NavMeshAgent navMeshAgent;
    private GameObject target;
    private float timeSinceLastTargetChange;
    private const float switchInterval = 5f;
    private bool isAnimating;

    public float minSpeed;
    public float maxSpeed;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent == null) return;

        InitializeNavMeshAgent();
        StartCoroutine(RandomSpeedCoroutine());
        FindClosestPlayer();
    }

    void Update()
    {
        

        if (target == null || !target.activeSelf)
        {
            FindClosestPlayer();
        }

        if (target != null)
        {
            navMeshAgent.destination = target.transform.position;
            if (!isAnimating && Vector3.Distance(transform.position, target.transform.position) <= animationTriggerDistance)
            {
                StartCoroutine(RandomAnimationsCoroutine());
            }
        }

        if (Time.time - timeSinceLastTargetChange >= switchInterval)
        {
            SwitchToRandomPlayer();
            timeSinceLastTargetChange = Time.time;
        }
    }

    private void InitializeNavMeshAgent()
    {
        navMeshAgent.speed = speed;
        navMeshAgent.stoppingDistance = 0f;
        navMeshAgent.angularSpeed = 360f;
    }

    private void FindClosestPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length == 0)
        {
            SetTarget(null);
            return;
        }

        float closestDistance = Mathf.Infinity;
        foreach (var player in players)
        {
            if (player.activeSelf)
            {
                float distance = Vector3.Distance(transform.position, player.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    SetTarget(player);
                }
            }
        }
    }

    private void SwitchToRandomPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 0)
        {
            SetTarget(players[Random.Range(0, players.Length)]);
        }
        else
        {
            SetTarget(null);
        }
    }

    private void SetTarget(GameObject newTarget)
    {
        target = newTarget;
        navMeshAgent.destination = newTarget != null ? newTarget.transform.position : transform.position;
    }

    private IEnumerator RandomSpeedCoroutine()
    {
        while (true)
        {
            navMeshAgent.speed = Random.Range(minSpeed, maxSpeed);
            yield return new WaitForSeconds(0.9f);
        }
    }

    private IEnumerator RandomAnimationsCoroutine()
    {
        isAnimating = true;
        if (animModel == null) yield break;

        string[] animations = { "Punch", "Stomp", "Throw" };
        string selectedAnimation = animations[Random.Range(0, animations.Length)];
        animModel.Play(selectedAnimation);

        yield return new WaitForSeconds(selectedAnimation == "Throw" ? 1.3f : 1.5f);

        if (selectedAnimation == "Throw")
        {
            ShootBullet();
        }

        isAnimating = false;
    }

    private void ShootBullet()
    {
        if (bulletPrefab == null || shootingPoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, shootingPoint.position, shootingPoint.rotation);
        if (bullet.TryGetComponent(out Rigidbody bulletRigidbody))
        {
            bulletRigidbody.AddForce(shootingPoint.forward * bulletForce, ForceMode.Impulse);
        }
    }

    // Gizmo drawing method
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Set color for the gizmo
        if (target != null)
        {
            Gizmos.DrawWireSphere(target.transform.position, animationTriggerDistance); // Draw a wireframe sphere at the target's position
        }
        else
        {
            Gizmos.DrawWireSphere(transform.position, animationTriggerDistance); // Draw a wireframe sphere around the GameObject's position
        }
    }

    
}
