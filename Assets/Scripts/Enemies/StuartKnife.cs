using System.Collections;
using UnityEngine;


public class StuartKnife : BaseEnemy
{
    public GameObject replacement;
    public GameObject jelloAmmo;

    private bool doingEffect = false;
    public Renderer skinRenderer;
    public Renderer helmetRenderer;
    public Material hitMaterial;

    public int hurtHayden = 3;

    public float dropChance = 0.30f;

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
        return 75;
    }

    //protected override int Health()
    //{
    //    return 3;
    //}

    protected override void Die()
    {
        // Generate a random float between 0 and 1
        float randomDropChance = Random.Range(0f, 1f);

        // Check if the generated number is less than or equal to 0.33
        if (randomDropChance <= dropChance)
        {
            // Spawn the poopAmmo
            Instantiate(jelloAmmo, transform.position, transform.rotation);
        }

        // Always spawn the replacement
        Instantiate(replacement, transform.position, transform.rotation);

        // Destroy the enemy game object
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
            yield return new WaitForSeconds(0.2f); // Wait for 2 seconds
            skinRenderer.material = original; // Restore original material
            helmetRenderer.material = original;
            doingEffect = false;
        }
    }


}
