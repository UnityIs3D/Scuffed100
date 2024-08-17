using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardPowerup : BasePowerup
{
    public GameObject WizardManPrefab; // Use a prefab instead of a direct GameObject reference
    private GameObject instantiatedWizard; // Store reference to instantiated object
    private string spawnPointName = "MegaSpawnPoint"; // Name of the spawn point GameObject

    protected override void StartPowerup()
    {

        GameObject spawnPoint = GameObject.Find(spawnPointName);

        if (WizardManPrefab != null && spawnPoint != null)
        {

            CapsuleCollider collider = GetComponent<CapsuleCollider>();
            if (collider != null)
            {
                collider.enabled = false;
            }

            // Instantiate the rockMan prefab at the spawn point's position and rotation
            instantiatedWizard = Instantiate(WizardManPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        }
    }

    protected override void StopPowerup()
    {
        // Implement logic to deactivate or destroy the instantiated rockMan object if it exists
        if (instantiatedWizard != null)
        {
            Destroy(instantiatedWizard);
        }
    }
}
