using System.Collections;
using UnityEngine;

public class CubeExplode : MonoBehaviour
{
    public int cubesPerAxis = 8;
    public float delay = 1f;
    public float force = 300f;
    public float radius = 2f;
    public GameObject explosionEffect;

    private void Start()
    {
        Invoke("Main", delay);
    }

    private void Main()
    {
        if (!this.enabled) return; // Check if the script is enabled

        for (int x = 0; x < cubesPerAxis; x++)
        {
            for (int y = 0; y < cubesPerAxis; y++)
            {
                for (int z = 0; z < cubesPerAxis; z++)
                {
                    CreateCube(new Vector3(x, y, z));
                }
            }
        }

        StartCoroutine(ExplodeBigCube());
    }

    private void CreateCube(Vector3 coordinates)
    {
        if (!this.enabled) return; // Check if the script is enabled

        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        Renderer rd = cube.GetComponent<Renderer>();
        rd.material = GetComponent<Renderer>().material;

        cube.transform.localScale = transform.localScale / cubesPerAxis;
        Vector3 firstCube = transform.position - transform.localScale / 2 + cube.transform.localScale / 2;
        cube.transform.position = firstCube + Vector3.Scale(coordinates, cube.transform.localScale);

        Rigidbody rb = cube.AddComponent<Rigidbody>();
        rb.mass = 0.1f;
        rb.AddExplosionForce(force, transform.position, radius);

        Collider collider = cube.AddComponent<BoxCollider>();

        CubeCollisionHandler collisionHandler = cube.AddComponent<CubeCollisionHandler>();
        collisionHandler.SetScriptEnabled(this.enabled); // Pass the enabled state to the collision handler
    }

    private IEnumerator ExplodeBigCube()
    {
        if (!this.enabled) yield break; // Check if the script is enabled

        explosionEffect.SetActive(true);
        GetComponent<MeshRenderer>().enabled = false;
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!this.enabled) return; // Check if the script is enabled

        if (other.gameObject.CompareTag("Purple"))
        {
            FixedJoint joint = gameObject.AddComponent<FixedJoint>();
            joint.connectedBody = other.rigidbody;

            
           
        }
    }
}

// Giving the miniCubes a script to handle collisions with EvilMinion
public class CubeCollisionHandler : MonoBehaviour
{
    

    private bool scriptEnabled = false;

    public void SetScriptEnabled(bool isEnabled)
    {
        scriptEnabled = isEnabled;
    }

    private void Update()
    {
        Destroy(gameObject , 5);
    }

    private void OnCollisionStay(Collision other)
    {
        if (!scriptEnabled) return; // Check if the script is enabled

        if (other.gameObject.CompareTag("Purple"))
        {
            var enemy = other.gameObject.GetComponent<BaseEnemy>();
            if (enemy)
            {
                enemy.OnHit();
            }

           
        }
    }
}
