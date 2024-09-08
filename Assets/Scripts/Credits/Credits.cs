using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Credits : MonoBehaviour
{
    public GameObject prefabToSpawn; // The prefab you want to spawn
    public float moveSpeed = 5f; // Speed at which the prefab will move upwards
    public float spawnInterval = 3f; // Time interval between spawning prefabs

    public Button homeButton; // Reference to the home button

    private void Start()
    {
        Time.timeScale = 1f; // Unpause the game

        if (homeButton != null)
        {
            homeButton.onClick.AddListener(OnHomeButtonClicked); // Add listener to the home button
        }

        StartCoroutine(SpawnInterval());
    }

    private void Update()
    {
        Cursor.visible = true;
        MovePrefabs();
    }

    private IEnumerator SpawnInterval()
    {
        while (true)
        {
            if (prefabToSpawn != null)
            {
                // Spawn a new prefab
                GameObject spawnedPrefab = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
                // Add the prefab to a list for movement
                PrefabMover prefabMover = spawnedPrefab.AddComponent<PrefabMover>();
                prefabMover.moveSpeed = moveSpeed;
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void MovePrefabs()
    {
        // Find all prefabs with the PrefabMover component and move them
        PrefabMover[] movers = FindObjectsOfType<PrefabMover>();
        foreach (PrefabMover mover in movers)
        {
            mover.transform.Translate(Vector3.up * mover.moveSpeed * Time.deltaTime);
        }
    }

    // Method to load the main menu scene
    private void OnHomeButtonClicked()
    {
        SceneManager.LoadScene("MainMenu"); // Replace "MainMenu" with the actual name of your main menu scene
    }
}

// A separate class to handle movement of each prefab
public class PrefabMover : MonoBehaviour
{
    public float moveSpeed;
}
