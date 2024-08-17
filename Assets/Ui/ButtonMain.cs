using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management
using UnityEngine.UI; // Required for UI components

public class ButtonMain : MonoBehaviour
{
    public AudioSource mouseClick; // Reference to the audio source for mouse click sound
    public Button tutorialButton;
    public Button waveButton; // Reference to the first button
    public Button warButton; // Reference to the second button

    void Start()
    {
        if(tutorialButton != null)
        {
            tutorialButton.onClick.AddListener(OnTutorialButtonClick);
        }

        if (waveButton != null)
        {
            waveButton.onClick.AddListener(OnWaveButtonClick);
        }

        if (warButton != null)
        {
            warButton.onClick.AddListener(OnWarButtonClick);
        }
    }

    private void Update()
    {
        Cursor.visible = true;
    }

    void OnTutorialButtonClick()
    {
        PlayClickSound();
        LoadTutoiral();
    }

    void OnWaveButtonClick()
    {
        PlayClickSound();
        LoadWaveGameScene();
    }

    void OnWarButtonClick()
    {
        PlayClickSound();
        LoadWarGameScene();
    }

    void PlayClickSound()
    {
        if (mouseClick != null)
        {
            mouseClick.Play();
        }
    }

    void LoadTutoiral()
    {
        SceneManager.LoadScene("Tutorial");
    }

    void LoadWaveGameScene()
    {
        SceneManager.LoadScene("WaveGame");
    }

    void LoadWarGameScene()
    {
        SceneManager.LoadScene("WarGame");
    }
}
