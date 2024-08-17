using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipPowerup : BasePowerup
{
    private GameObject gruAirShip;

    protected override void StartPowerup()
    {
        gruAirShip = GameObject.Find("ActiveGruAirShip").transform.Find("GruAirShip").gameObject;

        this.gameObject.GetComponent<BoxCollider>().enabled = false;//FakeAirShip small box collider off
        gruAirShip.SetActive(true);

    }

    protected override void StopPowerup()
    {
        gruAirShip.SetActive(false);
        
    }
}

