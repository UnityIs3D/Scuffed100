using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class WaterSeeBlue : MonoBehaviour
{
    public GameObject seeBlueEffect;
    public float gravityScaleOnTrigger = 0.5f;
    public float gravityIDK;

    public AudioSource splash;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            splash.Play();
            seeBlueEffect.SetActive(true);

            // Deactivate weapons and activate whale
            

            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                // Disable default gravity
                playerRb.useGravity = false;

                // Apply custom gravity (if needed)
                playerRb.AddForce(new Vector3(0, -gravityIDK * gravityScaleOnTrigger, 0), ForceMode.Acceleration);
            }
        }

        if (other.gameObject.CompareTag("Purple"))
        {
            // Destroy the object tagged as "Purple"
            Destroy(other.gameObject);
        }
    }
}
