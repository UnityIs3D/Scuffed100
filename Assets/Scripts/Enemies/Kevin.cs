using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Kevin : BaseEnemy
{
    public GameObject replacement;

    protected override float AttackDelay()
    {
        return 1f;
    }

    protected override void DoDamage(HealthDamage player)
    {
        player.TakeDamage(5);
    }

    
    protected override float KnockForce()
    {
        return 67;
    }

    protected override void Die()
    {
        Instantiate(replacement, transform.position, transform.rotation);
        Destroy(gameObject);
    }



}
