using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BossEnemy : BaseEnemy
{
    public Renderer skinRenderer;
    public Material hitMaterial;
    public GameObject purpleSplash;
    public CapsuleCollider elMachoCollider;
    private bool doingEffect = false;

    public GameObject poopSmoke;
    public GameObject posionGas;

    public TextMeshProUGUI healthText;

    public Boss bossWalkScript;

    

    private void Update()
    {
        UpdateHealthText();
    }

    

    protected override float AttackDelay()
    {
        return 1f;
    }

    protected override void DoDamage(HealthDamage player)
    {
        player.TakeDamage(20);
    }

    protected override float KnockForce()
    {
        return 100;
    }

    protected override void Hit()
    {
        if (!doingEffect)
        {
            StartCoroutine(RedSkinEffect());
        }
    }

    private IEnumerator RedSkinEffect()
    {
        var original = skinRenderer.material;

        doingEffect = true;
        skinRenderer.material = hitMaterial; // Change to red material  
        yield return new WaitForSeconds(0.2f); // Wait for 0.2 seconds  
        skinRenderer.material = original; // Restore original material  
        doingEffect = false;
    }

    protected override void Die()
    {
        poopSmoke.SetActive(true);
        posionGas.SetActive(true);

        bossWalkScript.enabled = false;

        var animator = GetComponent<Animator>();

        animator.Play("deathBoss");

        purpleSplash.SetActive(true);

        StartCoroutine(DieAfter(9));
    }

    private IEnumerator DieAfter(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }

   private void UpdateHealthText()  
   {  
      
        healthText.text = $"Steve: {health}";
   }  
}
