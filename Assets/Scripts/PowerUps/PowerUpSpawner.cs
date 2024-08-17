using System.Collections;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject[] powerUps;           // Array to hold different power-up prefabs
    public Transform[] spawnPoints;         // Array to hold spawn points

    public GameObject Arrow;                // Arrow prefab to spawn
    public float arrowYOffset = 1.0f;        // Y offset for the arrow's spawn position

    private GameObject currentPowerUp;      // Reference to the current spawned power-up
    private GameObject currentArrow;        // Reference to the current spawned arrow

    private void Start()
    {
        // Spawn the first power-up immediately
        SpawnPowerUp();
    }

    private void SpawnPowerUp()
    {
        if (powerUps.Length == 0 || spawnPoints.Length == 0)
        {
            return;
        }

        // Randomly select a power-up prefab and a spawn point
        GameObject powerUpPrefab = powerUps[Random.Range(0, powerUps.Length)];
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // Destroy existing power-up and arrow if they exist
        if (currentPowerUp != null)
        {
            Destroy(currentPowerUp);
        }
        if (currentArrow != null)
        {
            Destroy(currentArrow);
        }

        // Spawn the selected power-up prefab at the selected spawn point
        currentPowerUp = Instantiate(powerUpPrefab, spawnPoint.position, spawnPoint.rotation);
        currentArrow = SpawnArrowAtPowerUp(spawnPoint.position);
    }

    private GameObject SpawnArrowAtPowerUp(Vector3 powerUpPosition)
    {
        // Adjust the Y position of the spawn location for the arrow
        Vector3 arrowSpawnPosition = new Vector3(powerUpPosition.x, powerUpPosition.y + arrowYOffset, powerUpPosition.z);

        // Instantiate the arrow prefab at the adjusted position
        return Instantiate(Arrow, arrowSpawnPosition, Quaternion.identity);
    }

    private IEnumerator RespawnPowerUp()
    {
        // Wait before respawning the power-up
        yield return new WaitForSeconds(5f);

        // Spawn a new power-up
        SpawnPowerUp();
    }

    private void Update()
    {
        // Start coroutine to respawn power-up if it's destroyed
        if (currentPowerUp == null)
        {
            StartCoroutine(RespawnPowerUp());
        }
    }
}
