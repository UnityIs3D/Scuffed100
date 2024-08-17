using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//public class FartGunPowerup : BasePowerup
//{
//    private GameObject fartGun;
//    private GameObject jellyGun;


//    private void Start()
//    {
//        fartGun = GameObject.FindWithTag("FartGun").transform.Find("FartGun").gameObject;
//        jellyGun = GameObject.Find("JellyGun");

//    }

//    protected override void StartPowerup()
//    {
//        SetIcon(GameObject.FindGameObjectWithTag("IconCanvas").transform.Find("FartGunImage").gameObject);
//        jellyGun.SetActive(false);
//        fartGun.SetActive(true);
//    }

//    protected override void StopPowerup()
//    {
//        jellyGun.SetActive(true);
//        fartGun.SetActive(false);
//    }
//}





public class FartGunPowerup : BasePowerup
{
    private GameObject fartGun;
    private GameObject jellyGun;

    protected override void StartPowerup()
    {
        fartGun = GameObject.FindWithTag("FartGun").transform.Find("FartGun").gameObject;
        jellyGun = GameObject.Find("JellyGun");
        // Set the PooPCollider to inactive at the start
       

        // Assuming the icon needs to be set at the start of the powerup
        SetIcon(GameObject.FindGameObjectWithTag("IconCanvas").transform.Find("FartGunImage").gameObject);

        // Activate and deactivate the appropriate GameObjects
        jellyGun.SetActive(false);
        fartGun.SetActive(true);
    }

    protected override void StopPowerup()
    {
        // Restore the default state
        jellyGun.SetActive(true);
        fartGun.SetActive(false);
    }
}
