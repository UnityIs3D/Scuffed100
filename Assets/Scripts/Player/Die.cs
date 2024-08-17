using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public float dieTime = 3f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, dieTime);

    }
}
