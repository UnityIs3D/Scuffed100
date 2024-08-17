using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class PickUpThrow : MonoBehaviour
{
    public Rigidbody rb;
    private bool isHeld = false;
    public float throwForce = 22f;
    public Vector3 offset = new Vector3(0f, 0f, 2f); // Distance from camera

    

    // Activate the cubeExplode script
    public CubeExplode cubeExplodeScript; //💣💣💣💣💣💣💣💣

    private void Start()
    {
        cubeExplodeScript.enabled = false;
    }

    private void Update()
    {
        if (isHeld)
        {
            // Calculate target position based on camera position and offset
            Vector3 targetPos = Camera.main.transform.position +
                                Camera.main.transform.forward * offset.z +
                                Camera.main.transform.right * offset.x +
                                Camera.main.transform.up * offset.y;
            transform.position = targetPos;

            // Rotate object to face the camera
            transform.LookAt(Camera.main.transform.position);


            if (Input.GetKeyDown(KeyCode.E))//🌀🌀🌀🌀🌀🌀
            {
                ThrowObject();
            }

        }
    }

    private void OnCollisionEnter(Collision other)//🫳🫳🫳🫳🫳🫳🫳🫳🫳🫳
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!isHeld)
                PickUpObject();
            
        }
    }

    private void PickUpObject()
    {
        isHeld = true;
        rb.isKinematic = true; // Disable physics when held

        // Deactivate the CubeExplode script when picking up
        //if (cubeExplodeScript != null)
        //{
        //    cubeExplodeScript.enabled = false;
        //}
    }

    private void ThrowObject()
    {
        isHeld = false;
        rb.isKinematic = false; // Enable physics for throwing
        rb.velocity = Camera.main.transform.forward * throwForce; // Apply throw force

        // Activate the CubeExplode script when throwing
        if (cubeExplodeScript != null)
        {
            cubeExplodeScript.enabled = true;
        }
    }
}
