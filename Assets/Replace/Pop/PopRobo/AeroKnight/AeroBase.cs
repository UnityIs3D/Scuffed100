using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AeroBase : BaseEnemy
{
    public int hurtHadyen = 10;

    protected override float AttackDelay()
    {
        return 1f;
    }

    protected override void DoDamage(HealthDamage player)
    {
        player.TakeDamage(hurtHadyen);
    }

    protected override float KnockForce()
    {
        return 75;
    }

    public GameObject deadBody;
    //public GameObject poopAmmo;

    protected override void Die()
    {
        // Generate a random float between 0 and 1
        float randomDropChance = Random.Range(0f, 1f);

        // Check if the generated number is less than or equal to 0.33
        if (randomDropChance <= 0.33f)
        {
            // Spawn the poopAmmo
            //Instantiate(poopAmmo, transform.position, transform.rotation);
        }

        // Always spawn the replacement
        Instantiate(deadBody, transform.position, transform.rotation);

        // Destroy the enemy game object
        Destroy(gameObject);
    }

}
