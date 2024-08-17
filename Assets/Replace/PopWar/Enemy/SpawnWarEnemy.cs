using System.Collections.Generic;
using UnityEngine;
using System.Collections;


public class SpawnWarEnemy : MonoBehaviour
{
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;
    public GameObject enemy4Prefab;
    public GameObject enemy5Prefab;

    public int numberOfEnemy1 = 5;
    public int numberOfEnemy2 = 3;
    public int numberOfEnemy3 = 4;
    public int numberOfEnemy4 = 4;
    public int numberOfEnemy5 = 5;

    public Transform[] spawnPoints; // Array of predefined spawn points

    private List<GameObject> purpleEnemies = new List<GameObject>();

    //Door unlcok

    public GameObject unlockNextDoor;
    public GameObject nextEnemyCharge;

    public GameObject nexFriendsCharge;

    private void Start()
    {
        SpawnEnemies(enemy1Prefab, numberOfEnemy1);
        SpawnEnemies(enemy2Prefab, numberOfEnemy2);
        SpawnEnemies(enemy3Prefab, numberOfEnemy3);
        SpawnEnemies(enemy4Prefab, numberOfEnemy4);
        SpawnEnemies(enemy5Prefab, numberOfEnemy5);

        StartCoroutine(CheckForPurpleEnemies());
    }

    private void SpawnEnemies(GameObject enemyPrefab, int numberOfEnemies)
    {
        for (int i = 0; i < numberOfEnemies; i++)
        {
            // Ensure spawnPoints array is not empty
            if (spawnPoints.Length == 0)
            {

                return;
            }

            // Choose a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate the enemy at the chosen spawn point
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

            // If the enemy has the "Purple" tag, add it to the list
            if (enemy.CompareTag("Purple"))
            {
                purpleEnemies.Add(enemy);
            }
        }
    }

    private IEnumerator CheckForPurpleEnemies()
    {
        while (true)
        {
            // Remove null references (destroyed enemies)
            purpleEnemies.RemoveAll(enemy => enemy == null);

            // Check if all purple enemies have been destroyed
            if (purpleEnemies.Count == 0)
            {
                // Trigger your event here
                OnAllPurpleEnemiesDestroyed();
                yield break; // Stop the coroutine
            }

            // Wait for a short time before checking again
            yield return new WaitForSeconds(1f);
        }
    }

    private void OnAllPurpleEnemiesDestroyed()
    {
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();

        foreach (GameObject obj in allObjects)
        {
            // Check if the object's name matches the target name
            if (obj.name == "Friend")
            {
                // Destroy each game object
                Destroy(obj);


                StartCoroutine(SetNextChargeAttack());
            }
        }

        
    }


    private IEnumerator SetNextChargeAttack()
    {
        yield return new WaitForSeconds(5);
        Destroy(unlockNextDoor);

        nextEnemyCharge.SetActive(true);

        nexFriendsCharge.SetActive(true);
    }


}

