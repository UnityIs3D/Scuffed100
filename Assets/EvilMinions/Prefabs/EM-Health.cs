using System.Collections;
using UnityEngine;

public class EvilHealth : MonoBehaviour
{
    public int maxHealth = 200;
    public int currentHealth;
    public int takeDamage = 10;
    public float knockbackForce = 500f; // Adjust as needed
    public float knockbackDuration = 0.5f; // Duration in seconds to apply knockback

    private Rigidbody rb;
    private bool isPositionFrozen = true;

    public GameObject purpleBlood;
    public int purpleBloodDuration = 1;

    public GameObject deadBody;

    private void Start()
    {
        purpleBlood.SetActive(false);
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody>();
        FreezePosition();
    }

    private void FixedUpdate()
    {
        if (isPositionFrozen)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }

    private void FreezePosition()
    {
        isPositionFrozen = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePosition;
    }

    private void UnfreezePosition()
    {
        isPositionFrozen = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }

    private IEnumerator ApplyKnockback(Vector3 direction)
    {
        UnfreezePosition();

        rb.AddForce(direction * knockbackForce, ForceMode.Impulse);

        yield return new WaitForSeconds(knockbackDuration);

        FreezePosition();
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Sword"))
        {
            StartCoroutine(PurpleBloodDuration());
            TakeDamage(takeDamage);

            Vector3 moveDirection = (transform.position - other.transform.position).normalized;
            StartCoroutine(ApplyKnockback(moveDirection));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Sword"))
        {
            StartCoroutine(PurpleBloodDuration());
            TakeDamage(takeDamage);

            Vector3 moveDirection = (transform.position - other.transform.position).normalized;
            StartCoroutine(ApplyKnockback(moveDirection));
        }
    }

    private void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            BaseEnemy enemy = gameObject.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                enemy.OnHit();
            }

            Instantiate(deadBody, transform.position, transform.rotation);
            Destroy(gameObject);

            
        }
    }

    private IEnumerator PurpleBloodDuration()
    {
        purpleBlood.SetActive(true);
        yield return new WaitForSeconds(purpleBloodDuration);
        purpleBlood.SetActive(false);
    }
}

