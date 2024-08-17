using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KevinHulk : BaseEnemy
{
    

    public GameObject replacement;
    public int hurtHayden = 3;

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
        return 120;
    }

    protected override void Die()
    {
        Instantiate(replacement, transform.position, transform.rotation);
        Destroy(gameObject);
    }


}



