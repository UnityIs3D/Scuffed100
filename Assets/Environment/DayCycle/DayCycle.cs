using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public Material dayMaterial;
    public Material sunsetMaterial;
    public Material nightMaterial;

    private Material currentMaterial;
    private float exposure = 1.0f; // Initial exposure value
    private float exposureChangeRate = 0.05f; // Rate of exposure change per second

    private float timer = 0f;
    private float cycleDuration = 60f; // Total duration of one complete cycle (day + sunset + night)

    private enum DayPhase
    {
        Day,
        Sunset,
        Night
    }

    private DayPhase currentPhase = DayPhase.Day;

    void Start()
    {
        RenderSettings.skybox = dayMaterial;
        currentMaterial = dayMaterial;
        SetExposure(exposure);
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Calculate the phase and exposure based on time
        if (timer >= cycleDuration)
        {
            timer = 0f;
        }

        float t = timer / cycleDuration;

        if (t < 0.5f)
        {
            // Transition from day to sunset
            currentPhase = DayPhase.Day;
            float normalizedTime = t * 2f; // Normalize time from 0 to 1
            exposure = 1.0f - normalizedTime; // Decrease exposure from 1 to 0
            currentMaterial = dayMaterial;
        }
        else if (t < 0.75f)
        {
            // Transition from sunset to night
            currentPhase = DayPhase.Sunset;
            float normalizedTime = (t - 0.5f) * 4f; // Normalize time from 0 to 1
            exposure = normalizedTime; // Increase exposure from 0 to 1
            currentMaterial = sunsetMaterial;
        }
        else
        {
            // Transition from night to day
            currentPhase = DayPhase.Night;
            float normalizedTime = (t - 0.75f) * 4f; // Normalize time from 0 to 1
            exposure = 1.0f - normalizedTime; // Decrease exposure from 1 to 0
            currentMaterial = nightMaterial;
        }

        SetExposure(exposure);
        RenderSettings.skybox = currentMaterial;
    }

    void SetExposure(float exposureValue)
    {
        if (currentMaterial != null)
        {
            currentMaterial.SetFloat("_Exposure", exposureValue);
        }
    }
}
