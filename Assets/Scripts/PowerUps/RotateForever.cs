using UnityEngine;

public class RotateForever : MonoBehaviour
{
    public float rotationSpeed = 300f;

    void Update()
    {
        // Rotate the parent GameObject around the world up axis
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f, Space.Self);

        
    }
}
