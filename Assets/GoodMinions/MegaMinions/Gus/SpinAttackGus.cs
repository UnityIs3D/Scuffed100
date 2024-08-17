using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpinAttackGus : MonoBehaviour
{
    public float spinAttackSpeed = 100f; // Speed at which Gus spins
    public float movementSpeed = 5f; // Speed at which Gus moves towards targets

    private string targetTag = "Purple";
    private GameObject[] targets;
    private int currentTargetIndex = 0;
    private Transform currentTarget;

    
    

    void Start()
    {
        StartCoroutine(spinAttackMoveSpeed());

        targets = GameObject.FindGameObjectsWithTag(targetTag);

        if (targets.Length > 0)
        {
            currentTarget = targets[currentTargetIndex].transform;
        }
    }

    void Update()
    {
        transform.Rotate(0f, 0f, spinAttackSpeed * Time.deltaTime, Space.Self);





        // Check if the current target is null or destroyed, then move to the next target
        if (currentTarget == null || !currentTarget.gameObject.activeSelf)
        {
            NextTarget();
        }

        // Move towards the current target
        if (currentTarget != null)
        {
            Vector3 moveDirection = (currentTarget.position - transform.position).normalized;
            transform.position += moveDirection * movementSpeed * Time.deltaTime;
        }
    }

    void NextTarget()
    {
        // Find all gameObjects with the specified tag again in case new targets have been added
        targets = GameObject.FindGameObjectsWithTag(targetTag);

        // Check if there are targets available
        if (targets.Length == 0)
        {
            currentTarget = null;
            return;
        }

        // Increment the index to move to the next target
        currentTargetIndex++;

        // Wrap around if the index goes out of bounds
        if (currentTargetIndex >= targets.Length)
        {
            currentTargetIndex = 0;
        }

        // Update the current target
        currentTarget = targets[currentTargetIndex].transform;
    }

    private void OnCollisionStay(Collision collision)
    {
        // Check if the bullet collides with a purpleMinion
        if (collision.gameObject.CompareTag("Purple"))
        {
            var enemy = collision.gameObject.GetComponent<BaseEnemy>();
            if (enemy)
            {
                enemy.OnHit();
            }
        }
    }

    private IEnumerator spinAttackMoveSpeed()
    {
        while(true)
        {
            movementSpeed = Random.Range(50f, 90f);

            yield return new WaitForSeconds(4f);
        }
    }

}
