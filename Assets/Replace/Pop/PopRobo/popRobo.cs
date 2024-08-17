using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class popRobo : BaseEnemy
{
    public GameObject replacement;

    public Renderer skinRenderer;
    public Renderer helmetRenderer;
    public Material hitMaterial;
    private bool doingEffect = false;

    public int hurtHayden = 8;


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

    //protected override int Health()
    //{
    //    return 7;
    //}

    protected override void Die()
    {
        float randomDropChance = Random.Range(0f, 1f);
        if (randomDropChance <= 0.33f)
        {
            // Instantiate poopAmmo or any other drop if necessary
        }

        Instantiate(replacement, transform.position, transform.rotation);
        Destroy(gameObject);
    }

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
            helmetRenderer.material = hitMaterial;
            yield return new WaitForSeconds(0.3f); // Wait for 2 seconds
            skinRenderer.material = original; // Restore original material
            helmetRenderer.material = original;
            doingEffect = false;
        }
    }




}
