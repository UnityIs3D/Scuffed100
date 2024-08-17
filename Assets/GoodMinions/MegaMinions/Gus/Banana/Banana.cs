using System.Collections;
using System.Collections.Generic;
using UnityEngine;





public class Banana : MonoBehaviour
{
    private Transform enemy;  // Reference to the enemy's Transform component
    public float speed = 5f; // Speed at which this object moves towards the enemy

    private void Start()
    {
        FindEnemy();
    }

    private void FindEnemy()
    {
        // Attempt to find the enemy object with the tag "Purple"
        GameObject enemyObject = GameObject.FindWithTag("Purple");

        // Check if we found an object with the tag
        if (enemyObject != null)
        {
            enemy = enemyObject.transform; // Assign the transform component of the enemy
        }
        else
        {
            Debug.Log("No object with tag 'Purple' found.");
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

        Destroy(gameObject, 4);
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
