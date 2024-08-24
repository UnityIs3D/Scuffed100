using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireFollow : MonoBehaviour
{
    private Transform target; // Reference to the target's Transform component (enemy or boss)
    public float speed = 50f; // Speed at which this object moves towards the target
    public float dieFireBall = 4f;

    public ParticleSystem explosionEffectPrefab; // Reference to the explosion effect prefab

    private MeshRenderer skinFireBall;
    

    private void Start()
    {
        skinFireBall = GetComponent<MeshRenderer>();

        // Destroy the fireball after dieFireBall seconds if it hasn't collided with anything
        Destroy(gameObject, dieFireBall);

        FindTarget();
    }

    private void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");

        if (enemies.Length == 0)
        {
            Debug.Log("No enemies found");
            target = null;
            return;
        }

        float closestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemyObject in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemyObject.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = enemyObject;
            }
        }

        if (closestEnemy != null)
        {
            target = closestEnemy.transform; // Assign the transform component of the closest enemy
        }
    }

    private void FindBoss()
    {
        GameObject boss = GameObject.Find("Boss");

        speed = 80;
        if (boss == null)
        {
            Debug.Log("Boss not found");
            target = null;
            return;
        }

        target = boss.transform; // Assign the transform component of the boss
    }

    void Update()
    {
        if (target != null)
        {
            // Calculate the direction to move towards the target
            Vector3 direction = target.position - transform.position;

            // Move towards the target
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else
        {
            // If target is lost (destroyed or not found), try to find it again
            FindTarget();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            skinFireBall.enabled = false;
            // Instantiate the explosion effect at the fireball's position and rotation
            Instantiate(explosionEffectPrefab, transform.position, transform.rotation);

            // Destroy the fireball game object
            Destroy(gameObject,1);
            
        }

        if (other.gameObject.CompareTag("Sword"))
        {
            
            // Change the tag of the fireball to "Player"
            this.gameObject.tag = "GoodBall";
            

            FindBoss();
        }

        if (target != null && other.transform == target)
        {
            // If the fireball collides with the target (boss)
            if (target.gameObject.CompareTag("Purple"))
            {
                var enemy = target.gameObject.GetComponent<BaseEnemy>();
                if (enemy)
                {
                    enemy.OnHit();
                }
                // Instantiate the explosion effect at the fireball's position and rotation
                Instantiate(explosionEffectPrefab, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
}
