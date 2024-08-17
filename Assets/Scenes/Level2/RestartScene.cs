using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartScene : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Scene currentScene = SceneManager.GetActiveScene();
            
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
