using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class GruAirShip : MonoBehaviour
{
    private string targetTag = "Purple";
    public float moveSpeed = 5f;
    public float orbitRadius = 10f;
    public float orbitSpeed = 2f;
    public float heightOffset;

    private Transform currentTarget;
    private float angle = 0f;

    void Start()
    {
        StartCoroutine(GenerateRandomHeightOffset());
        FindNextTarget();
    }

    IEnumerator GenerateRandomHeightOffset()
    {
        while (true)
        {
            // Generate a random heightOffset between 13 and 50
            heightOffset = Random.Range(13f, 50f);

            // Wait for 1 second before generating a new offset
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
}
