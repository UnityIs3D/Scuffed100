using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;


public class BasePowerup : MonoBehaviour
{
    private GameObject Icon;
    public float Duration = 0;
    private float Timer = 0;
    private bool Enabled = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Enabled = true;
            if (Icon)
            {
                Icon.SetActive(true);
            }
            Timer = Duration;
            StartPowerup();




        }
    }

    protected void SetIcon(GameObject icon)
    {
        Icon = icon;
        if(icon)
        {
            Icon.SetActive(true);
        }
    }

    private void Update()
    {
        if (Enabled)
        {
            Timer -= Time.deltaTime;
            PowerupUpdate();
            if (Timer <= 0)
            {
                Enabled = false;
                if (Icon)
                {
                    Icon.SetActive(false);
                }
                StopPowerup();
                Destroy(gameObject);
            }
        }
    }

    protected virtual string PowerupTag()
    {
        throw new System.NotImplementedException();
    }

    protected virtual void StartPowerup()
    {
        throw new System.NotImplementedException();
    }

    protected virtual void PowerupUpdate()
    {
        //throw new System.NotImplementedException();
    }

    protected virtual void StopPowerup()
    {

        throw new System.NotImplementedException();
    }

    

    

}
