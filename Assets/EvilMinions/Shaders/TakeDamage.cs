using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class TakeDamage : MonoBehaviour
{

    public Volume volume; // Reference to the Volume component in your scene
    private Vignette vignette; // Reference to the Vignette effect settings

    public float intensity = 0.5f; // Initial intensity value for the vignette effect

    void Start()
    {
        // Check if the Volume component is assigned
        if (volume == null)
        {
            Debug.LogError("Volume not assigned!");
            enabled = false; // Disable this script if the volume is not assigned
            return;
        }

        // Initialize and get the Vignette effect from the volume
        volume.profile.TryGet(out vignette);

        // Set initial intensity
        vignette.intensity.value = intensity;
    }

    void Update()
    {
        // Example: Control intensity with the mouse scroll wheel (you can use any method to update intensity)
        float scrollWheel = Input.GetAxis("Mouse ScrollWheel");
        intensity += scrollWheel * 0.1f; // Adjust this value according to your needs

        // Clamp intensity to a reasonable range
        intensity = Mathf.Clamp(intensity, 0f, 1f);

        // Apply intensity to the Vignette effect
        vignette.intensity.value = intensity;
    }


}
