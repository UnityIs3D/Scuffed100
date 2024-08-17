using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GoodMinionHealth : MonoBehaviour
{
    public int maxHealth = 200;
    public int currentHealth;
    public int takeDamage = 10;
    public float knockbackForce = 500f; // Adjust as needed
    public float knockbackDuration = 0.5f; // Duration in seconds to apply knockback

    private Rigidbody rb;
    private bool isPositionFrozen = true;
    private Vector3 frozenPosition;

    public GameObject yellowBlood;
    public GameObject replacement;
   

    private void Start()
    {
        this.gameObject.name = "Friend";

        yellowBlood.SetActive(false);

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



    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Purple"))
        {
            StartCoroutine(YellowBloodDuration());
            TakeDamage(takeDamage);

            // Calculate the direction to move back
            Vector3 moveDirection = (transform.position - other.transform.position).normalized;

            // Start coroutine to apply knockback
            StartCoroutine(ApplyKnockback(moveDirection));
        }
    }

    private void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Instantiate(replacement, transform.position, transform.rotation);
        
        Destroy(gameObject);
    }

    private IEnumerator YellowBloodDuration()
    {
        yellowBlood.SetActive(true);
        yield return new WaitForSeconds(1f);
        yellowBlood.SetActive(false);
    }
}
