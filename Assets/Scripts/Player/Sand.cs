using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Sand : MonoBehaviour
{

    //public GameObject weapons;
    public GameObject whale;
    public GameObject crossHair;


    private string objectName = "Aero";

    // OnCollisionEnter should not be defined inside Update()
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                // Check if the object's name matches the specified name
                if (obj.name == objectName)
                {
                    // Destroy the object
                    Destroy(obj);
                }
            }

            //weapons.SetActive(false);
            whale.SetActive(true);
            crossHair.SetActive(false);

            Rigidbody otherRigidbody = other.gameObject.GetComponent<Rigidbody>();
            otherRigidbody.constraints = RigidbodyConstraints.FreezePosition;
        }

        if (other.gameObject.CompareTag("Purple"))
        {
            Destroy(other.gameObject);
        }
    }
}



