using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGoodGuysWar : MonoBehaviour
{
    public GameObject friend1Prefab;
    public GameObject friend2Prefab;
    public GameObject friend3Prefab;
    public GameObject friend4Prefab;
    public GameObject friend5Prefab;

    public int numberOfFriend1 = 5;
    public int numberOfFriend2 = 3;
    public int numberOfFriend3 = 4;
    public int numberOfFriend4 = 4;
    public int numberOfFriend5 = 4;

    public Transform[] spawnPoints; // Array of predefined spawn points

    private List<GameObject> friendsTagPlayer = new List<GameObject>();

    //Door unlcok

    private GameObject unlockNextDoor;
    private GameObject nextChargeAttack;

    private void Start()
    {
        SpawnFriends(friend1Prefab, numberOfFriend1);
        SpawnFriends(friend2Prefab, numberOfFriend2);
        SpawnFriends(friend3Prefab, numberOfFriend3);
        SpawnFriends(friend4Prefab, numberOfFriend4);
        SpawnFriends(friend5Prefab, numberOfFriend5);

        StartCoroutine(CheckForFriends());
    }

    private void SpawnFriends(GameObject friendPrefab, int numberOfFriends)
    {
        for (int i = 0; i < numberOfFriends; i++)
        {
            // Ensure spawnPoints array is not empty
            if (spawnPoints.Length == 0)
            {

                return;
            }

            // Choose a random spawn point
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Instantiate the enemy at the chosen spawn point
            GameObject friend = Instantiate(friendPrefab, spawnPoint.position, Quaternion.identity);

            // If the enemy has the "Purple" tag, add it to the list
            if (friend.name == "Friend")
            {
                friendsTagPlayer.Add(friend);
            }
        }
    }

    private IEnumerator CheckForFriends()
    {
        while (true)
        {
            // Remove null references (destroyed enemies)
            friendsTagPlayer.RemoveAll(friend => friend == null);

            // Check if all purple enemies have been destroyed
            if (friendsTagPlayer.Count == 0)
            {
                // Trigger your event here
                AllFriendsDestoyed();
                yield break; // Stop the coroutine
            }

            // Wait for a short time before checking again
            yield return new WaitForSeconds(1f);
        }
    }

    private void AllFriendsDestoyed()
    {
        //Destroy(unlockNextDoor);

        //nextChargeAttack.SetActive(true);
    }
}
