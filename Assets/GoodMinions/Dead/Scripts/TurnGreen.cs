using System.Collections;
using System.Collections.Generic;
using UnityEngine;






public class TurnGreen : MonoBehaviour
{
    public GameObject greenMinionPrefab; // Assign this in the Inspector
    public GameObject green1EyeMinion;

    private void OnCollisionEnter(Collision other)
    {
        // Check if the poopCollider touches with a purpleMinion
        if (other.gameObject.CompareTag("Purple"))
        {
            int randomGreenMinion = Random.Range(0, 2);

            if(randomGreenMinion == 0)
            {
                Instantiate(greenMinionPrefab, other.transform.position, other.transform.rotation);
                Destroy(other.gameObject);
            }
            else
            {
                Instantiate(green1EyeMinion, other.transform.position, other.transform.rotation);
                Destroy(other.gameObject);
            }

            
           
        }
    }
}

