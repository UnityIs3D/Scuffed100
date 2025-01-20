using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyclopPowerup : BasePowerup
{
    public GameObject cyclops; // Use a prefab instead of a direct GameObject reference
    private GameObject instantiatedCyclops; // Store reference to instantiated object
    private string spawnPointName = "MegaSpawnPoint"; // Name of the spawn point GameObject

    public GameObject modelCapsule;
    public GameObject toyFigure;

    protected override void StartPowerup()
    {
        modelCapsule.SetActive(false);
        toyFigure.SetActive(false);

        GameObject spawnPoint = GameObject.Find(spawnPointName);

        if (cyclops != null && spawnPoint != null)
        {

            CapsuleCollider collider = GetComponent<CapsuleCollider>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            // Instantiate the rockMan prefab at the spawn point's position and rotation
            instantiatedCyclops = Instantiate(cyclops, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }

    protected override void StopPowerup()
    {
        // Implement logic to deactivate or destroy the instantiated rockMan object if it exists
        if (instantiatedCyclops != null)
        {
            Destroy(instantiatedCyclops);
        }
    }


}
