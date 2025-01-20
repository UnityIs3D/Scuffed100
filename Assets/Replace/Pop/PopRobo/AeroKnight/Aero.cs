using System.Collections;
using UnityEngine;
using TMPro;

public class Aero : MonoBehaviour
{
    private const string TargetTag = "Player";
    public float moveSpeed = 5f;
    public float orbitRadius = 10f;
    public float orbitSpeed = 2f;
    public float heightOffset;
    public float stoppingDistance = 2f; // Distance at which Aero stops moving towards the player

    public GameObject bulletPrefab;
    public float shootInterval = 1f;
    public float bulletSpeed = 10f;

    private Transform currentTarget;
    private float angle = 0f;
    private float shootTimer;
    private bool isMovingTowardsTarget = false;
    private bool isIdle = false; // State to track idle status

    public Animator anim;
    public GameObject laserFire;

    private ScoreManager scoreManager;

    public float laserActivationDistance = 15f; // Distance within which the laser should be active

    private void Start()
    {
        this.gameObject.name = "Aero";
        scoreManager = FindObjectOfType<ScoreManager>();

        laserFire.SetActive(false);

        StartCoroutine(GenerateRandomHeightAndSpeedOffset());
        FindNextTarget();
        StartCoroutine(OrbitAndMoveCoroutine());
    }

    private IEnumerator GenerateRandomHeightAndSpeedOffset()
    {
        while (true)
        {
            moveSpeed = Random.Range(15f, 24f);
            heightOffset = Random.Range(13f, 25f);
            yield return new WaitForSeconds(7f);
        }
    }

    private IEnumerator OrbitAndMoveCoroutine()
    {
        while (true)
        {
            if (currentTarget == null)
            {
                FindNextTarget();
                yield return null;
                continue;
            }

            isMovingTowardsTarget = false;
            yield return new WaitForSeconds(8f);

            isMovingTowardsTarget = true;
            float startTime = Time.time;
            Vector3 directionToTarget = Vector3.zero;

            while (Time.time - startTime < 8f && currentTarget != null)
            {
                moveSpeed = 50;

                directionToTarget = (currentTarget.position - transform.position).normalized;
                transform.position += directionToTarget * moveSpeed * Time.deltaTime;

                if (Vector3.Distance(transform.position, currentTarget.position) <= stoppingDistance)
                {
                    isMovingTowardsTarget = false;
                    isIdle = true;
                    break;
                }

                yield return null;
            }

            if (currentTarget != null)
            {
                transform.position = currentTarget.position - directionToTarget * stoppingDistance;
            }

            // Ensure Aero stays idle for 3 seconds
            if (isIdle)
            {
                yield return new WaitForSeconds(3f);
                isIdle = false;
            }

            FindNextTarget();
        }
    }

    private void Update()
    {
        if (currentTarget == null)
        {
            FindNextTarget();
            return;
        }

        if (!isMovingTowardsTarget && !isIdle)
        {
            Vector3 targetPosition = currentTarget.position + Vector3.up * heightOffset;
            Vector3 orbitOffset = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * orbitRadius;
            Vector3 desiredPosition = targetPosition + orbitOffset;
            transform.position = Vector3.MoveTowards(transform.position, desiredPosition, moveSpeed * Time.deltaTime);

            angle += orbitSpeed * Time.deltaTime;
        }

        // Ensure Aero looks at the player even while orbiting or idling
        Vector3 lookAtPosition = new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z);
        transform.LookAt(lookAtPosition);

        // Check distance and activate/deactivate the laser
        float distanceToTarget = Vector3.Distance(transform.position, currentTarget.position);
        laserFire.SetActive(distanceToTarget <= laserActivationDistance);

        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    private void FindNextTarget()
    {
        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag(TargetTag);

        if (targetObjects.Length > 0)
        {
            int randomIndex = Random.Range(0, targetObjects.Length);
            currentTarget = targetObjects[randomIndex].transform;
        }
        else
        {
            currentTarget = null;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || currentTarget == null)
            return;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (bulletRb != null)
        {
            Vector3 shootDirection = (currentTarget.position - transform.position).normalized;
            bulletRb.velocity = shootDirection * bulletSpeed;
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

    public GameObject deadBody;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Wizard")|| other.gameObject.CompareTag("Marsh"))
        {
            Instantiate(deadBody, transform.position, transform.rotation);

            // Destroy the enemy game object
            Destroy(gameObject);
        }
    }
}
