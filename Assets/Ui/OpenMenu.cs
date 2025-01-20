using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Assign this in the Inspector
    public Button resumeButton;
    public Button homeButton;

    private bool isPaused = false;

    public AudioSource mouseClick;

    public PlayerMovement playerMovement;
    public GunSystem gunSystem;

    private void Start()
    {
        playerMovement.enabled = true;
        gunSystem.enabled = true;
        Time.timeScale = 1f;

        // Ensure buttons are interactable
        resumeButton.interactable = true;
        homeButton.interactable = true;

        // Add listeners to buttons
        resumeButton.onClick.AddListener(Resume);
        homeButton.onClick.AddListener(GoToMainMenu);

        // Ensure pauseMenuUI is inactive at the start
        pauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Tab))
        {
            if (isPaused)
            {
                Resume();
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                Pause();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
    }

    public void Resume()
    {

        Cursor.visible = false;
        playerMovement.enabled = true;
        gunSystem.enabled = true;
        if (mouseClick != null) mouseClick.Play();
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause()
    {
        Cursor.visible = true;
        if (mouseClick != null) mouseClick.Play();
        pauseMenuUI.SetActive(true);
        isPaused = true;
        gunSystem.enabled = false;
        playerMovement.enabled = false;
        Time.timeScale = 0f;
    }

    public void GoToMainMenu()
    {
        if (mouseClick != null) mouseClick.Play();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        if (mouseClick != null) mouseClick.Play();
        // Optionally, you might want to do more here, like saving progress
        // For now, we'll just quit the game
        Application.Quit();
        // In the Editor, you can use UnityEditor.EditorApplication.isPlaying = false;
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
