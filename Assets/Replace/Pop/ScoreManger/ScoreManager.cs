using UnityEngine;
using TMPro; // Ensure you have the correct namespace for TextMeshPro
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // Assign this in the inspector if you're using a UI text element
    private int score;
    private Dictionary<string, int> objectScores; // Dictionary to hold scores for different object names

    

    void Start()
    {
       
        score = 0;
        UpdateScoreUI();

        // Initialize the dictionary with object names and their corresponding scores
        objectScores = new Dictionary<string, int>
        {
            { "Pop-1", 3 },
            { "Pop-2", 5 },
            { "PopSkeleton", 1},
            { "PopJumper", 8},
            { "PopRobot", 12},
            { "Aero", 15},
            {"ElMacho", 14}
            // Add more objects and their scores here
        };
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    public void HandleObjectDestroyed(GameObject destroyedObject)
    {
        // Check if the destroyed object's name is in the dictionary and add the corresponding score
        if (objectScores.TryGetValue(destroyedObject.name, out int scoreAmount))
        {
            AddScore(scoreAmount);
        }
    }
}
