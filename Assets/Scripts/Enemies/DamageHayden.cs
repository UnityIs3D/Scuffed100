using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageHayden : BaseEnemy
{
    public GameObject replacementDeadBadGuy;
    public int knockHaydenForce = 120;
    public int damageHayden = 20;



    protected override float AttackDelay()
    {
        return 1f;
    }

    protected override void DoDamage(HealthDamage player)
    {
        player.TakeDamage(damageHayden);
    }

    protected override float KnockForce()
    {
        return knockHaydenForce;
    }

    protected override void Die()
    {
        Instantiate(replacementDeadBadGuy, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    //protected override int Health()
    //{
    //    return enemyHealth;
    //}

}
