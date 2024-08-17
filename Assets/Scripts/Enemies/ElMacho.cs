using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class ElMacho : BaseEnemy
{
    

    public Renderer skinRenderer;
    public Material hitMaterial;
    public GameObject purpleSplash;
    public CapsuleCollider elMachoCollider;
    private bool doingEffect = false;

    public GameObject grendaeBullet;

    public int hurtHayden = 12;

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
        return 200;
    }

    //protected override int Health()
    //{
    //    return 9;
    //}

    protected override void Hit()
    {
        if (!doingEffect)
        {
            StartCoroutine(RedSkinEffect());
        }

        IEnumerator RedSkinEffect()
        {
            var original = skinRenderer.material;

            doingEffect = true;
            skinRenderer.material = hitMaterial; // Change to red material
            yield return new WaitForSeconds(0.2f); // Wait for 2 seconds
            skinRenderer.material = original; // Restore original material
            doingEffect = false;
        }
    }




    protected override void Die()
    {
        float randomDropChance = Random.Range(0f, 1f);

        // Check if the generated number is less than or equal to 0.33
        if (randomDropChance <= 0.60f)
        {
            // Spawn the poopAmmo
            Instantiate(grendaeBullet, transform.position, transform.rotation);
        }

        gameObject.tag = "Nothing";

        var animator = GetComponent<Animator>();
        var navAgent = GetComponent<NavMeshAgent>();

        animator.Play("deathBoss");
        navAgent.enabled = false;
        purpleSplash.SetActive(true);
        elMachoCollider.enabled = false;



        StartCoroutine(DieAfter(4));

        IEnumerator DieAfter(float seconds)
        {

            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }
    }
}
