using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BaseEnemy : MonoBehaviour
{
    private float attackDebounce = 0;
    public float health;
    public float maxHealth;
    public AudioSource minecraftSound;
    public GameObject screenBloodEffect;

    public GameObject greenBody;

    private void Start()
    {
        health = maxHealth;

        
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            minecraftSound.Play(); // Minecraft hit sound
            StartCoroutine(ScreenBloodDuration());// ScreenBloodDamage

            if (attackDebounce <= 0)
            {
                // Deal damage to player
                var healthDamage = other.gameObject.GetComponent<HealthDamage>();
                DoDamage(healthDamage);

                // Calculate the direction from the enemy to the player
                Vector3 knockbackDirection = other.transform.position - transform.position;
                knockbackDirection.Normalize();
                knockbackDirection.y = 0.2f;

                // Apply knockback force to the player in the opposite direction
                other.gameObject.GetComponent<Rigidbody>().AddForce(knockbackDirection * KnockForce(), ForceMode.Impulse);

                attackDebounce = AttackDelay();
            }
        }

        else if (other.gameObject.CompareTag("PoopCollider"))
        {
            Debug.Log("EWWW");
            Instantiate(greenBody, other.transform.position, other.transform.rotation);
            Destroy(gameObject);
        }

        else if (other.gameObject.CompareTag("bullet") || other.gameObject.CompareTag("Sword"))
        {
            var enemy = other.gameObject.GetComponent<BaseEnemy>();
            if (enemy)
            {
                enemy.Die();
            }
        }

        else if (other.gameObject.CompareTag("GoodBall"))
        {
            health -= 10;
        }

       
    }

    

    public void OnHit()
    {
        health -= 1;
        if (health <= 0)
        {
            Die();
        } else
        {
            Hit();
        }
    }

    public IEnumerator ScreenBloodDuration()
    {
        screenBloodEffect.SetActive(true);
        yield return new WaitForSeconds(1);
        screenBloodEffect.SetActive(false);
    }

    private void Update()
    {
        attackDebounce -= Time.deltaTime;

        
    }

    protected virtual float AttackDelay()
    {
        throw new System.NotImplementedException();
    }

    protected virtual void DoDamage(HealthDamage player)
    {
        throw new System.NotImplementedException();
    }

    

    protected virtual float KnockForce()
    {
        throw new System.NotImplementedException();
    }

    protected virtual void Hit()
    {

    }

    protected virtual void Die()
    {
        throw new System.NotImplementedException();
    }


    protected virtual void ScoreUp()
    {
        throw new System.NotImplementedException();
    }


    
}




