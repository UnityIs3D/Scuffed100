//using UnityEngine;
//using UnityEngine.SceneManagement; // Required for scene management
//using UnityEngine.UI; // Required for UI components

//public class ButtonMain : MonoBehaviour
//{
//    public AudioSource mouseClick; // Reference to the audio source for mouse click sound
//    public Button tutorialButton;
//    public Button waveButton; // Reference to the first button
//    public Button warButton; // Reference to the second button
//    public Button CreditsButton;

//    void Start()
//    {
//        if(tutorialButton != null)
//        {
//            tutorialButton.onClick.AddListener(OnTutorialButtonClick);
//        }

//        if (waveButton != null)
//        {
//            waveButton.onClick.AddListener(OnWaveButtonClick);
//        }

//        if (warButton != null)
//        {
//            warButton.onClick.AddListener(OnWarButtonClick);
//        }

//        if (CreditsButton != null)
//        {
//            CreditsButton.onClick.AddListener(OnCreditsButtonClick);
//        }
//    }

//    private void Update()
//    {
//        Cursor.visible = true;
//    }

//    void OnTutorialButtonClick()
//    {
//        PlayClickSound();
//        LoadTutoiral();
//    }

//    void OnWaveButtonClick()
//    {
//        PlayClickSound();
//        LoadWaveGameScene();
//    }

//    void OnWarButtonClick()
//    {
//        PlayClickSound();
//        LoadWarGameScene();
//    }

//    void OnCreditsButtonClick()
//    {
//        PlayClickSound();
//        CreditScreenLoad();

//    }

//    void PlayClickSound()
//    {
//        if (mouseClick != null)
//        {
//            mouseClick.Play();
//        }
//    }

//    void LoadTutoiral()
//    {
//        SceneManager.LoadScene("Tutorial");
//    }

//    void LoadWaveGameScene()
//    {
//        SceneManager.LoadScene("WaveGame");
//    }

//    void LoadWarGameScene()
//    {
//        SceneManager.LoadScene("WarGame");
//    }

//    void CreditScreenLoad()
//    {
//        SceneManager.LoadScene("Credits");
//    }
//}


using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management
using UnityEngine.UI; // Required for UI components

public class ButtonMain : MonoBehaviour
{
    public AudioSource mouseClick; // Reference to the audio source for mouse click sound
    public Button tutorialButton;
    public Button waveButton; // Reference to the first button
    public Button warButton; // Reference to the second button
    public Button creditsButton; // Note the changed name to follow convention

    void Start()
    {
        // Add listeners to buttons if they are assigned
        if (tutorialButton != null)
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

        if (creditsButton != null)
        {
            creditsButton.onClick.AddListener(OnCreditsButtonClick);
        }

        // Set the cursor to be visible at start if desired
        Cursor.visible = true;
    }

    void OnTutorialButtonClick()
    {
        PlayClickSound();
        LoadTutorial();
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

    void OnCreditsButtonClick()
    {
        PlayClickSound();
        LoadCreditsScreen();
    }

    void PlayClickSound()
    {
        if (mouseClick != null)
        {
            mouseClick.Play();
        }
    }

    void LoadTutorial()
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

    void LoadCreditsScreen()
    {
        SceneManager.LoadScene("Credits");
    }
}
