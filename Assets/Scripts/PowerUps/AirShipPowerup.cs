using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirShipPowerup : BasePowerup
{
    private GameObject gruAirShip;
    

    public GameObject skin;

    public GameObject fireTrail1;
    public GameObject fireTrail2;

    protected override void StartPowerup()
    {
        fireTrail1.SetActive(false);
        fireTrail2.SetActive(false);
        skin.SetActive(false);

        gruAirShip = GameObject.Find("ActiveGruAirShip").transform.Find("GruAirShip").gameObject;

        this.gameObject.GetComponent<BoxCollider>().enabled = false;//FakeAirShip small box collider off
        gruAirShip.SetActive(true);

    }

    protected override void StopPowerup()
    {
        gruAirShip.SetActive(false);
        
    }
}

