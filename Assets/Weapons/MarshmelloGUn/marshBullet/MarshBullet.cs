using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarshBullet : MonoBehaviour
{
    public GameObject objectToInstantiate; // The prefab to instantiate
    public int numberOfObjects = 10; // Number of objects to instantiate
    public float explosionForce = 10f; // The force of the explosion
    public float explosionRadius = 5f; // Radius of the explosion
    public float upwardModifier = 1f; // How much force is applied upwards
    public Transform spawnPoint; // The point where the objects are instantiated
    public float miniMarshDealy;

    private void Start()
    {
        // Call the function to instantiate and apply the explosion force
        StartCoroutine(DelayExplodeMini());
    }

    
    private IEnumerator  DelayExplodeMini()
    {
        yield return new WaitForSeconds(miniMarshDealy);

        for (int i = 0; i < numberOfObjects; i++)
        {
            // Randomly position the object around the spawn point within the radius
            Vector3 spawnPosition = spawnPoint.position + Random.insideUnitSphere * explosionRadius;

            // Instantiate the object at the calculated position
            GameObject obj = Instantiate(objectToInstantiate, spawnPosition, Quaternion.identity);

            // Add a Rigidbody if it doesn't already have one
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = obj.AddComponent<Rigidbody>(); // Add Rigidbody if missing
            }

            // Apply explosion force to the Rigidbody
            Vector3 explosionDirection = obj.transform.position - spawnPoint.position;
            float distance = explosionDirection.magnitude;
            float force = Mathf.Lerp(explosionForce, 0f, distance / explosionRadius); // Decrease force with distance

            rb.AddForce(explosionDirection.normalized * force + Vector3.up * upwardModifier, ForceMode.Impulse);
        }
    }


    private FixedJoint joint; // Declare a FixedJoint variable

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("Purple"))
        {
            // Check if the joint doesn't already exist
            if (joint == null)
            {
                joint = gameObject.AddComponent<FixedJoint>();
                joint.connectedBody = other.rigidbody;
            }

            Destroy(gameObject, 12);
        }
    }

    




}
