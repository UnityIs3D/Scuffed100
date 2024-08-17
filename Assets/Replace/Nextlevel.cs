using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Nextlevel : MonoBehaviour
{
    void Update()
    {
        // Check if the "N" key is pressed
        if (Input.GetKeyDown(KeyCode.N))
        {
            // Load the next scene
            // You can replace "NextSceneName" with the name of the scene you want to load
            SceneManager.LoadScene("Level2");
        }
    }
}
