using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Stuart : BaseEnemy
{
    public GameObject replacement;
    public GameObject heartReplacement;



    public float dropChance = 0.30f;

    public int hurtHayden = 1;


    protected override float AttackDelay()
    {
        return 1f;
    }

    protected override void DoDamage(HealthDamage player)
    {
        player.TakeDamage(hurtHayden);
    }

    protected override float KnockForce()
    {
        return 50;
    }

    protected override void Die()
    {
        float randomDropChance = Random.Range(0f, 1f);

        // Check if the generated number is less than or equal to 0.33
        if (randomDropChance <= dropChance)
        {
            // Spawn the poopAmmo
            Instantiate(heartReplacement, transform.position, transform.rotation);
        }

        Instantiate(replacement, transform.position, transform.rotation);
        Destroy(gameObject);
    }

  


}
