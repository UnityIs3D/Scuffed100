using UnityEngine;
using UnityEngine.UI;

public class SliderSens : MonoBehaviour
{
    [Header("Assignables")]
    [Tooltip("Reference to the UI Slider.")]
    public Slider sensitivitySlider;

    [Tooltip("Reference to the PlayerMovement script.")]
    public PlayerMovement playerMovement;

    private void Start()
    {
        if (sensitivitySlider != null && playerMovement != null)
        {
            // Set the slider value to the current sensitivity
            sensitivitySlider.value = playerMovement.sensitivity;

            // Add a listener to call the OnSliderValueChanged method when the slider value changes
            sensitivitySlider.onValueChanged.AddListener(OnSliderValueChanged);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        if (playerMovement != null)
        {
            // Update the sensitivity in the PlayerMovement script
            playerMovement.sensitivity = value;
        }
    }
}
