using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Whale : BaseEnemy
{
    private Transform player;
    public float moveSpeed = 5f;
    public Animator whaleAnimation;
    public float attackDistance = 3;

    public AudioSource whaleAttackSound;
    private bool hasPlayedAttackSound = false;  // Flag to track if attack sound has been played
    public AudioSource whaleIsComing;

    public GameObject replacement;

    void Start()
    {
        player = GameObject.FindWithTag("MainCamera").transform;
        whaleIsComing.Play();
    }

    void Update()
    {
        // Calculate direction from whale to player
        Vector3 directionToPlayer = player.position - transform.position;

        // Calculate direction from player to whale
        Vector3 directionToWhale = transform.position - player.position;

        // Rotate the whale to look at the player
        transform.rotation = Quaternion.LookRotation(directionToPlayer);

        // Rotate the player to look at the whale
        player.rotation = Quaternion.LookRotation(directionToWhale);

        // Move the whale towards the player
        transform.Translate(directionToPlayer.normalized * moveSpeed * Time.deltaTime, Space.World);

        if (directionToPlayer.magnitude <= attackDistance && !hasPlayedAttackSound)
        {
            moveSpeed = 8;

            whaleAnimation.Play("Gulp");
            whaleAttackSound.Play();
            hasPlayedAttackSound = true;  // Set flag to true once sound is played
        }
    }


    protected override void DoDamage(HealthDamage player)
    {
        player.TakeDamage(1000000);
    }
    protected override float AttackDelay()
    {
        return 1f;
    }

    protected override float KnockForce()
    {
        return 75;
    }

    //protected override int Health()
    //{
    //    return 1000000000;
    //}

    protected override void Die()
    {
        Instantiate(replacement, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
