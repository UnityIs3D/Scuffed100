using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxerManPowerup : BasePowerup
{
    public GameObject boxerManPrefab; // Use a prefab instead of a direct GameObject reference
    private GameObject instantiatedBoxer; // Store reference to instantiated object
    private string spawnPointName = "MegaSpawnPoint"; // Name of the spawn point GameObject

    public GameObject modelCapsule;
    public GameObject figureToy;

    protected override void StartPowerup()
    {
        modelCapsule.SetActive(false);
        figureToy.SetActive(false);

        GameObject spawnPoint = GameObject.Find(spawnPointName);

        if (boxerManPrefab != null && spawnPoint != null)
        {

            CapsuleCollider collider = GetComponent<CapsuleCollider>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            // Instantiate the rockMan prefab at the spawn point's position and rotation
            instantiatedBoxer = Instantiate(boxerManPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }

    protected override void StopPowerup()
    {
        // Implement logic to deactivate or destroy the instantiated rockMan object if it exists
        if (instantiatedBoxer != null)
        {
            Destroy(instantiatedBoxer);
        }
    }
}
