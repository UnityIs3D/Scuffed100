using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public GameObject hayden; // Reference to the player GameObject
    public GameObject deathPanel; // Reference to the UI panel that shows the death screen
    public Button restartButton; // Reference to the restart button
    public Button homeButton; // Reference to the home button

    public PlayerMovement playerMoveScript; // Reference to the player movement script
    public OpenMenu pauseMenu;

    public AudioSource mouseClick;

    private string objectName = "Friend";


    private void Start()
    {

        // Ensure deathPanel and buttons are initially inactive
        deathPanel.SetActive(false);

        // Add listeners to buttons
        restartButton.onClick.AddListener(RestartGame);
        homeButton.onClick.AddListener(GoToMainMenu);
    }

    private void Update()
    {
        // Check if hayden (player) is inactive
        if (!hayden.activeInHierarchy)
        {
            Cursor.visible = true;

            if (playerMoveScript.enabled)
            {
                playerMoveScript.enabled = false;
                Cursor.lockState = CursorLockMode.None;

                playerMoveScript.enabled = false;
                // Show the death panel and pause the game
                deathPanel.SetActive(true);
                //Time.timeScale = 0f; // Pause the game
                pauseMenu.enabled = false;
            }
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in allObjects)
            {
                // Check if the object's name matches the specified name
                if (obj.name == objectName)
                {
                    // Destroy the object
                    Destroy(obj);
                }
            }
        }
    }

    // Method to restart the game
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

    // Method to go to the main menu
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
