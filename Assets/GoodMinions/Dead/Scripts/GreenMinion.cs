using System.Collections;
using UnityEngine;

public class GreenMinion : MonoBehaviour
{
    public float delayInSeconds = 5f;  // Adjust this to the delay you want before pausing the animation
    public Animator poopDeath;          // Reference to the Animator component

    private bool hasPaused = false;     // Flag to ensure animation is paused only once

    void Start()
    {
        // Check if the Animator component is assigned
        if (poopDeath == null)
        {
            Debug.LogError("Animator component is not assigned!");
            enabled = false; // Disable the script if Animator is not assigned
        }
        else
        {
            StartCoroutine(PauseAnimationAfterDelayCoroutine());
        }
    }

    IEnumerator PauseAnimationAfterDelayCoroutine()
    {
        yield return new WaitForSeconds(delayInSeconds);

        if (!hasPaused)
        {
            poopDeath.speed = 0f; // Pause the animation by setting speed to 0
            hasPaused = true;
            Debug.Log("GreenAnimation paused after " + delayInSeconds + " seconds.");
        }
    }
}
