using UnityEngine;

public class HealthPowerup : BasePowerup
{
    private HealthDamage healthPlayerScript;


    protected override void StartPowerup()
    {
        healthPlayerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthDamage>();
        healthPlayerScript.TakeDamage(-10);
    }

    protected override void StopPowerup()
    {
        // No specific action needed here for a health power-up
    }
}
