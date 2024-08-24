using System.Collections;
using UnityEngine;
using TMPro;
public class Boss : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float rotationSpeed = 5f;
    public float stoppingDistance = 1f;
    public float fireTriggerDistance = 2f;
    public Animator bossAnim;
    public GameObject fireBreath;
    public float orbitRadius = 10f;
    public float orbitSpeed = 2f;
    public float heightOffset;
    public GameObject bulletPrefab;
    public float shootInterval = 1f;
    public float bulletSpeed = 10f;

    private Rigidbody rb;
    private Transform currentTarget;
    private float angle = 0f;
    private float shootTimer;
    private bool isFlying;

    public GameObject flyTrail1;
    public GameObject flyTrail2;

    public Animator bossDance;
    public Boss bossWalkScript;
    public GameObject playerHayden;

    public GameObject deflectFireballDirection;
    

    
    

    private void Start()
    {
        StartCoroutine(DurationFireBallDeflect());

        rb = GetComponent<Rigidbody>();
        fireBreath.SetActive(false);
        StartCoroutine(GenerateRandomHeightAndSpeedOffset());
        FindNextTarget();
        StartCoroutine(WalkAndFly());
    }

    private void Update()
    {
        

        if (!playerHayden.activeSelf)
        {
            bossWalkScript.enabled = false;
            bossDance.Play("ArmDance");
        }

        if (isFlying)
        {
            FlyBehavior();
            flyTrail1.SetActive(true);
            flyTrail2.SetActive(true);
        }
        else
        {
            WalkBehavior();
            flyTrail1.SetActive(false);
            flyTrail2.SetActive(false);
        }
    }


    

    private void WalkBehavior()
    {
        rb.useGravity = true;
        bossAnim.Play("El-RUn");

        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stoppingDistance)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.deltaTime);
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (distanceToPlayer <= fireTriggerDistance)
        {
            StartCoroutine(ActivateFireBreath());
        }
    }

    private void FlyBehavior()
    {
        rb.useGravity = false;
        bossAnim.Play("Float");

        if (currentTarget == null)
        {
            FindNextTarget();
            return;
        }

        Vector3 targetPosition = currentTarget.position + Vector3.up * heightOffset;
        Vector3 orbitOffset = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * orbitRadius;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition + orbitOffset, moveSpeed * Time.deltaTime);

        transform.LookAt(new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z));

        angle += orbitSpeed * Time.deltaTime;

        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            StartCoroutine(FireAndReturnToFloat());
            shootTimer = 0f;
        }
    }

    private IEnumerator ActivateFireBreath()
    {
        if (fireBreath.activeSelf) yield break;

        bossAnim.SetTrigger("Scream");
        fireBreath.SetActive(true);

        float animationLength = bossAnim.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(Mathf.Min(5f, animationLength));

        fireBreath.SetActive(false);
    }

    private IEnumerator FireAndReturnToFloat()
    {
        bossAnim.SetTrigger("Float");
        yield return new WaitForSeconds(2.2f);

        Shoot();
    }

    private IEnumerator GenerateRandomHeightAndSpeedOffset()
    {
        while (true)
        {
            moveSpeed = Random.Range(20f, 33f);
            heightOffset = Random.Range(13f, 50f);
            yield return new WaitForSeconds(1f);
        }
    }

    private void FindNextTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        if (targets.Length > 0)
        {
            currentTarget = targets[Random.Range(0, targets.Length)].transform;
        }
        else
        {
            currentTarget = null;
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || currentTarget == null) return;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector3 shootDirection = (currentTarget.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;
    }

    private IEnumerator WalkAndFly()
    {
        while (true)
        {
            isFlying = false;
            yield return new WaitForSeconds(7f);

            isFlying = true;
            yield return new WaitForSeconds(22f);
        }
    }

    public float knockbackForce = 88;



    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Sword"))
        {
            ApplyKnockback(collision);
        }
    }


    private void ApplyKnockback(Collision collision)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Calculate the direction of the knockback force
            Vector3 knockbackDirection = (transform.position - collision.transform.position).normalized;

            // Apply force to the Rigidbody
            rb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
        }
    }

    private IEnumerator DurationFireBallDeflect()
    {
        deflectFireballDirection.SetActive(true);
        yield return new WaitForSeconds(5);
        deflectFireballDirection.SetActive(false);
        yield break;

    }



}
