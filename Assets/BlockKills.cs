using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockKills : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Purple"))
        {
            Destroy(other.gameObject);
        }
    }
}
