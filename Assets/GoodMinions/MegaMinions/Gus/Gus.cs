using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Gus : MonoBehaviour
{
    private string targetTag = "Purple";
    public float moveSpeed = 5f;
    public float orbitRadius = 10f;
    public float orbitSpeed = 2f;
    public float heightOffset;

    public GameObject bulletPrefab;
    public float shootInterval = 1f;
    public float bulletSpeed = 10f;

    private Transform currentTarget;
    private float angle = 0f;
    private float shootTimer;




    void Start()
    {
        StartCoroutine(GenerateRandomHeightAndSpeedOffset());
        FindNextTarget();

        // Start the spin attack coroutine



    }

    IEnumerator GenerateRandomHeightAndSpeedOffset()
    {
        while (true)
        {
            moveSpeed = Random.Range(20f, 33f);

            // Generate a random heightOffset between 13 and 80
            heightOffset = Random.Range(13f, 50f);

            // Wait for 5 seconds before generating a new offset
            yield return new WaitForSeconds(1f);
        }
    }


    void Update()
    {
        if (currentTarget == null)
        {
            FindNextTarget();
            return;
        }

        // Move towards the target with orbital motion
        Vector3 targetPosition = currentTarget.position + Vector3.up * heightOffset;
        Vector3 orbitOffset = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle)) * orbitRadius;
        Vector3 desiredPosition = targetPosition + orbitOffset;
        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, moveSpeed * Time.deltaTime);

        // Rotate to face the target
        Vector3 lookAtPosition = new Vector3(currentTarget.position.x, transform.position.y, currentTarget.position.z);
        transform.LookAt(lookAtPosition);

        angle += orbitSpeed * Time.deltaTime;

        // Shooting logic
        shootTimer += Time.deltaTime;
        if (shootTimer >= shootInterval)
        {
            Shoot();
            shootTimer = 0f;
        }
    }

    void FindNextTarget()
    {
        GameObject[] targetObjects = GameObject.FindGameObjectsWithTag(targetTag);

        if (targetObjects.Length > 0)
        {
            int randomIndex = Random.Range(0, targetObjects.Length);
            currentTarget = targetObjects[randomIndex].transform;
        }
        else
        {
            currentTarget = null;
        }
    }

    void Shoot()
    {
        if (bulletPrefab == null || currentTarget == null)
            return;

        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        Vector3 shootDirection = (currentTarget.position - transform.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = shootDirection * bulletSpeed;
    }





}




