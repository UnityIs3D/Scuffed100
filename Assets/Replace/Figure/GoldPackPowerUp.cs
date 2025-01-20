using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldPackPowerUp : BasePowerup
{
    public GameObject goldCapsule;
    public GameObject glass;
    public GameObject glassYellow;

    protected override void StartPowerup()
    {

        glass.SetActive(false);
        glassYellow.SetActive(false);

    }

    protected override void StopPowerup()
    {
        Destroy(goldCapsule);

    }
}
