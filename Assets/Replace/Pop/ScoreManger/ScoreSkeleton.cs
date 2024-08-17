using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AI;

public class ScoreSkeleton : MonoBehaviour
{

    private ScoreManager scoreManager; // Reference to the ScoreManager

    private GameObject player;
    private NavMeshAgent navMeshMoveScript;  
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshMoveScript = GetComponent<NavMeshAgent>();
        this.gameObject.name = "PopSkeleton";
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (player != null)
        {
            // Check if the player GameObject is inactive
            if (!player.activeSelf)
            {
                navMeshMoveScript.enabled = false;
                
                
                
            }
        }
    }

    private void OnDestroy()
    {
        // This method is called when the GameObject is destroyed
        if (scoreManager != null)
        {
            scoreManager.HandleObjectDestroyed(gameObject);
        }
    }
}
