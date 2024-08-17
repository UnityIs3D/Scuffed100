using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockManPowerup : BasePowerup
{
    public GameObject rockManPrefab; // Use a prefab instead of a direct GameObject reference
    private GameObject instantiatedRockMan; // Store reference to instantiated object
    private string spawnPointName = "MegaSpawnPoint"; // Name of the spawn point GameObject

    protected override void StartPowerup()
    {
        
        GameObject spawnPoint = GameObject.Find(spawnPointName);

        if (rockManPrefab != null && spawnPoint != null)
        {
            
            CapsuleCollider collider = GetComponent<CapsuleCollider>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            // Instantiate the rockMan prefab at the spawn point's position and rotation
            instantiatedRockMan = Instantiate(rockManPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }

    protected override void StopPowerup()
    {
        // Implement logic to deactivate or destroy the instantiated rockMan object if it exists
        if (instantiatedRockMan != null)
        {
            Destroy(instantiatedRockMan);
        }
    }
}
