//using System.Collections;
//using UnityEngine;
//using TMPro;

//public class EnemySpawner : MonoBehaviour
//{
//    public GameObject[] enemyPrefabs; // Array to hold enemy prefabs  
//    public Transform[] spawnPoints; // Array of spawn points  
//    public float timeBetweenWaves = 5f; // Time between waves  
//    public TextMeshProUGUI waveText; // UI element for wave count  
//    public TextMeshProUGUI enemiesCountText; // UI element for enemy count  
//    public int baseEnemiesPerWave = 8; // Base number of enemies per wave  

//    private int currentWave = 0; // Current wave index  
//    private bool waveInProgress = false;

//    void Start()
//    {
//        // Check for necessary components  
//        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0 || waveText == null || enemiesCountText == null)
//        {

//            return;
//        }

//        StartCoroutine(SpawnWaves());
//    }

//    IEnumerator SpawnWaves()
//    {
//        while (true)
//        {
//            if (!waveInProgress)
//            {
//                waveInProgress = true;
//                currentWave++;
//                UpdateWaveText(currentWave);
//                yield return StartCoroutine(SpawnEnemiesForWave(currentWave));
//                yield return new WaitForSeconds(timeBetweenWaves);
//                waveInProgress = false;
//            }
//            yield return null;
//        }
//    }

//    void UpdateWaveText(int waveNumber)
//    {
//        waveText.text = $"Wave: {waveNumber}";
//    }

//    IEnumerator SpawnEnemiesForWave(int waveNumber)
//    {
//        int numberOfEnemies = baseEnemiesPerWave + (waveNumber - 1) * 8; // Calculate enemies for this wave  
//        for (int i = 0; i < numberOfEnemies; i++)
//        {
//            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
//            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
//            GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity, transform); // Make the spawned enemy a child of EnemySpawner  
//            yield return new WaitForSeconds(0.5f); // Adjust if needed based on spawn rate  
//        }
//        // Wait until all spawned enemies are destroyed  
//        yield return new WaitUntil(() => CountEnemiesWithTag("Purple") == 0);
//        waveInProgress = false;
//    }

//    void Update()
//    {
//        UpdateEnemiesCountText();
//    }

//    void UpdateEnemiesCountText()
//    {
//        int enemyCount = CountEnemiesWithTag("Purple");
//        enemiesCountText.text = $"Enemies: {enemyCount}";
//    }

//    int CountEnemiesWithTag(string tag)
//    {
//        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
//        int count = 0;
//        foreach (var enemy in enemies)
//        {
//            if (enemy != null && enemy.activeInHierarchy)
//            {
//                count++;
//            }
//        }
//        return count;
//    }
//}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawner : MonoBehaviour
{
    [Header("Configuration")]
    public GameObject[] enemyPrefabs; // Array to hold enemy prefabs  
    public Transform[] spawnPoints; // Array of spawn points  
    public float timeBetweenWaves = 5f; // Time between waves  
    public TextMeshProUGUI waveText; // UI element for wave count  
    public TextMeshProUGUI enemiesCountText; // UI element for enemy count  
    public int baseEnemiesPerWave = 8; // Base number of enemies per wave  
    public int enemiesIncreasePerWave = 8; // Number of additional enemies per wave

    private int currentWave = 0; // Current wave index  
    private bool waveInProgress = false;
    private List<GameObject> activeEnemies = new List<GameObject>(); // Track active enemies

    void Start()
    {
        if (enemyPrefabs.Length == 0 || spawnPoints.Length == 0 || waveText == null || enemiesCountText == null)
        {
            Debug.LogError("EnemySpawner is missing some required references.");
            enabled = false;
            return;
        }

        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        while (true)
        {
            if (!waveInProgress)
            {
                waveInProgress = true;
                currentWave++;
                UpdateWaveText(currentWave);
                yield return StartCoroutine(SpawnEnemiesForWave(currentWave));
                yield return new WaitForSeconds(timeBetweenWaves);
                waveInProgress = false;
            }
            yield return null;
        }
    }

    private void UpdateWaveText(int waveNumber)
    {
        waveText.text = $"Wave: {waveNumber}";
    }

    private IEnumerator SpawnEnemiesForWave(int waveNumber)
    {
        int numberOfEnemies = baseEnemiesPerWave + (waveNumber - 1) * enemiesIncreasePerWave;

        for (int i = 0; i < numberOfEnemies; i++)
        {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject enemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
            GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity, transform);
            activeEnemies.Add(enemyInstance); // Track the newly spawned enemy
            yield return new WaitForSeconds(0.5f); // Adjust spawn rate if needed
        }

        // Wait until all spawned enemies are destroyed
        yield return new WaitUntil(() => activeEnemies.TrueForAll(enemy => enemy == null || !enemy.activeInHierarchy));
        activeEnemies.RemoveAll(enemy => enemy == null); // Clean up references
        waveInProgress = false;
    }

    private void Update()
    {
        UpdateEnemiesCountText();
    }

    private void UpdateEnemiesCountText()
    {
        int enemyCount = CountActiveEnemiesWithTag("Purple");
        enemiesCountText.text = $"Enemies: {enemyCount}";
    }

    private int CountActiveEnemiesWithTag(string tag)
    {
        int count = 0;
        foreach (var enemy in activeEnemies)
        {
            if (enemy != null && enemy.activeInHierarchy && enemy.CompareTag(tag))
            {
                count++;
            }
        }
        return count;
    }
}
