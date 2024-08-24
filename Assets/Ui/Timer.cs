using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText; // Assign this in the Inspector
    public GameObject boss; // Assign this in the Inspector
    public GameObject winPanel; // Reference to the win panel UI
    public Button restartButton; // Reference to the restart button
    public Button homeButton; // Reference to the home button
    public AudioSource mouseClick; // Audio source for button clicks

    private float timeElapsed;
    private bool isGameRunning;
    private bool isTimerRunning; // Flag to control timer updates

    public PlayerMovement playerMoveScript;
    public GameObject openMenuManger;

    public GameObject hayden;
    public GameObject bossNameTitle;

    



    void Start()
    {
        
        bossNameTitle.SetActive(false);
        // Initialize the timer
        timeElapsed = 0f;
        isGameRunning = true;
        isTimerRunning = true; // Start the timer initially

        // Check if the boss is not null
        if (boss == null)
        {
            Debug.LogError("Boss GameObject is not assigned.");
        }

        // Ensure winPanel and buttons are initially inactive
        if (winPanel != null)
        {
            winPanel.SetActive(false);
        }

        // Add listeners to buttons
        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartGame);
        }
        if (homeButton != null)
        {
            homeButton.onClick.AddListener(GoToMainMenu);
        }

      
    }

    void Update()
    {
        if(boss.activeSelf)
        {
            
            bossNameTitle.SetActive(true);
        }

        if (isGameRunning)
        {
            // Update the timer only if it's running
            if (isTimerRunning)
            {
                timeElapsed += Time.deltaTime;
                UpdateTimerText();
            }
        }

        // Check if the boss has been destroyed
        if (boss == null)
        {
            EndGame();
        }

        // Check if Hayden is inactive and stop the timer if so
        if (!hayden.activeSelf)
        {
            isTimerRunning = false; // Stop the timer
        }
        else
        {
            isTimerRunning = true; // Resume the timer
        }
    }

    private void UpdateTimerText()
    {
        if (timerText != null)
        {
            // Format the time as minutes and seconds
            int minutes = Mathf.FloorToInt(timeElapsed / 60f);
            int seconds = Mathf.FloorToInt(timeElapsed % 60f);
            timerText.text = $"{minutes:D2}:{seconds:D2}";
        }
    }

    private void EndGame()
    {
        if (isGameRunning)
        {
            isGameRunning = false;
            // Show the win panel and pause the game
            if (winPanel != null)
            {
                winPanel.SetActive(true);
            }
            // Pause the game
            Cursor.lockState = CursorLockMode.None;
            playerMoveScript.enabled = false;
            Time.timeScale = 0f;
            Destroy(openMenuManger);
        }
    }

    public void RestartGame()
    {
        if (mouseClick != null)
        {
            mouseClick.Play();
        }
        Time.timeScale = 1f; // Unpause the game
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    public void GoToMainMenu()
    {
        if (mouseClick != null)
        {
            mouseClick.Play();
        }
        Time.timeScale = 1f; // Unpause the game
        SceneManager.LoadScene("MainMenu");
    }

   
}
