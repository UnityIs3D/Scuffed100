using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class FastShoePowerup : BasePowerup
{
    private PlayerMovement playerMovementScript;
    public AudioSource speedBoostSound;
   

    protected override void StartPowerup()
    {
        this.gameObject.GetComponent<BoxCollider>().enabled = false;// small box collider off

        SetIcon(GameObject.FindGameObjectWithTag("IconCanvas").transform.Find("Shoeimage").gameObject);
   
        playerMovementScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        playerMovementScript.maxSpeed = 100f;
        speedBoostSound.Play();


    }

    protected override void StopPowerup()
    {
        playerMovementScript.maxSpeed = 35f;

    }
}
