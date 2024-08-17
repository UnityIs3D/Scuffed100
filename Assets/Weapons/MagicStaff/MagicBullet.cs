using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBullet : MonoBehaviour
{
    private Transform enemy; // Reference to the enemy's Transform component
    private float speed = 50f; // Speed at which this object moves towards the enemy

    private void Start()
    {
        FindEnemy();
        // Destroy the bullet after 4 seconds if it hasn't collided with anything
        Destroy(gameObject, 4f);
    }

    private void FindEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Purple");

        if (enemies.Length == 0)
        {
            Debug.Log("No enemies found");
            enemy = null;
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
            enemy = closestEnemy.transform; // Assign the transform component of the closest enemy
        }
    }

    void Update()
    {
        if (enemy != null)
        {
            // Calculate the direction to move towards the enemy
            Vector3 direction = enemy.position - transform.position;

            // Move towards the enemy
            transform.position = Vector3.MoveTowards(transform.position, enemy.position, speed * Time.deltaTime);
        }
        else
        {
            // If enemy is lost (destroyed or not found), try to find it again
            FindEnemy();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet collides with a purpleMinion
        if (collision.gameObject.CompareTag("Purple"))
        {
            BaseEnemy enemy = collision.gameObject.GetComponent<BaseEnemy>();
            if (enemy != null)
            {
                enemy.OnHit();
            }

            Destroy(gameObject);
        }
    }
}

