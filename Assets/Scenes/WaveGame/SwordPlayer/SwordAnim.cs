using System.Collections;
using UnityEngine;

public class SwordAnim : BaseGun
{
    public enum GunState
    {
        Shooting,
        Reloading,
        None
    }

    public float attackDelay = 0.5f; // Delay between attacks
    public Animator armModel;
    public CapsuleCollider swordCapsuleCollider;
    public BoxCollider swordBoxCollider;
    public float colliderActivationTime = 0.1f; // Time when the collider is active during the attack

    private int swingCount = 0;
    private bool isAttacking = false;

    public GameObject icon;

    private GunState state = GunState.None;

    private void Start()
    {
        // Disable both colliders initially
        if (swordCapsuleCollider != null)
        {
            swordCapsuleCollider.enabled = false;
        }

        if (swordBoxCollider != null)
        {
            swordBoxCollider.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Change "Fire1" to your desired input button
        {
            if (!isAttacking)
            {
                StartCoroutine(AttackSequence());
            }
        }

        GameObject bananaSwordIcon = GameObject.Find("Sword-Pos");

        if (bananaSwordIcon != null)
        {
            Transform bananaSwordTransform = bananaSwordIcon.transform.Find("Arms");

            if (bananaSwordTransform != null && !bananaSwordTransform.gameObject.activeSelf)
            {
                
                icon.SetActive(false);
            }
            else
            {
                icon.SetActive(true);
            }
        }
    }

    private IEnumerator AttackSequence()
    {
        isAttacking = true;

        // Play the appropriate attack animation
        if (swingCount == 0)
        {
            armModel.Play("Attack1");
            swingCount = 1;
        }
        else
        {
            armModel.Play("Attack2");
            swingCount = 0; // Reset to 0 after Attack2
        }

        // Activate colliders for the duration of the attack
        ActivateColliders(true);
        yield return new WaitForSeconds(colliderActivationTime);
        ActivateColliders(false);

        // Wait for the duration of the attack animation
        yield return new WaitForSeconds(attackDelay);

        isAttacking = false;
    }

    private void ActivateColliders(bool activate)
    {
        if (swordCapsuleCollider != null)
        {
            swordCapsuleCollider.enabled = activate;
        }

        if (swordBoxCollider != null)
        {
            swordBoxCollider.enabled = activate;
        }
    }

    public override void ThrowGun()
    {
        //throw new System.NotImplementedException();
    }

    public override void Reload(bool instant)
    {
        //throw new System.NotImplementedException();
    }

    public override void Shoot()
    {
        //throw new System.NotImplementedException();
    }

    public override bool IsBlocking()
    {
        return state != GunState.None;
    }
}
