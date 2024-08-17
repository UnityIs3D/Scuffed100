using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinDamage : MonoBehaviour
{
    private bool doingEffect = false;
    public Renderer skinRenderer;
    public Renderer helmetRenderer;
    public Material hitMaterial;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Sword")|| other.gameObject.CompareTag("bullet"))
        {
            if (!doingEffect)
            {
                StartCoroutine(RedSkinEffect());
            }

            IEnumerator RedSkinEffect()
            {
                var original = skinRenderer.material;

                doingEffect = true;
                skinRenderer.material = hitMaterial; // Change to red material
                helmetRenderer.material = hitMaterial;
                yield return new WaitForSeconds(0.6f); // Wait for 2 seconds
                skinRenderer.material = original; // Restore original material
                helmetRenderer.material = original;
                doingEffect = false;
            }
        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Sword"))
        {
            if (!doingEffect)
            {
                StartCoroutine(RedSkinEffect());
            }

            IEnumerator RedSkinEffect()
            {
                var original = skinRenderer.material;

                doingEffect = true;
                skinRenderer.material = hitMaterial; // Change to red material
                helmetRenderer.material = hitMaterial;
                yield return new WaitForSeconds(0.6f); // Wait for 2 seconds
                skinRenderer.material = original; // Restore original material
                helmetRenderer.material = original;
                doingEffect = false;
            }
        }


    }
}
