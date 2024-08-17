using UnityEngine;

public class SmokeJump : MonoBehaviour
{
    public ParticleSystem jumpSmoke;
    private bool hasTouchedGround = false;

    // Define the layer mask for the ground layer
    public LayerMask groundLayerMask;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object collided with the ground layer
        if (IsGroundLayer(collision.gameObject.layer))
        {
            if (!hasTouchedGround)
            {
                PlaySmoke();
                hasTouchedGround = true;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Reset the flag when leaving the ground
        if (IsGroundLayer(collision.gameObject.layer))
        {
            hasTouchedGround = false;
        }
    }

    private void PlaySmoke()
    {
        if (jumpSmoke != null)
        {
            jumpSmoke.Play();
        }
    }

    private bool IsGroundLayer(int layer)
    {
        return (groundLayerMask & (1 << layer)) != 0;
    }
}
