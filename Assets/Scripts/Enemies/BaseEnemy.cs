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
    

    private void Start()
    {
        health = maxHealth;

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            minecraftSound.Play(); // Minecraft hit sound
            StartCoroutine(ScreenBloodDuration());// ScreenBloodDamage

            if (attackDebounce <= 0)
            {
                // Deal damage to player
                var healthDamage = collision.gameObject.GetComponent<HealthDamage>();
                DoDamage(healthDamage);

                // Calculate the direction from the enemy to the player
                Vector3 knockbackDirection = collision.transform.position - transform.position;
                knockbackDirection.Normalize();
                knockbackDirection.y = 0.2f;

                // Apply knockback force to the player in the opposite direction
                collision.gameObject.GetComponent<Rigidbody>().AddForce(knockbackDirection * KnockForce(), ForceMode.Impulse);

                attackDebounce = AttackDelay();
            }
        }
        else if (collision.gameObject.CompareTag("bullet") || collision.gameObject.CompareTag("Sword"))
        {
            var enemy = collision.gameObject.GetComponent<BaseEnemy>();
            if (enemy)
            {
                enemy.Die();
            }
        }

        else if (collision.gameObject.CompareTag("GoodBall"))
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




