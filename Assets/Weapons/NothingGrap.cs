using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NothingGrap : BaseGun
{
    public enum GunState
    {
        Shooting,
        Reloading,
        None
    }

    private GunState state = GunState.None;


    public override void Reload(bool instant)
    {
        //throw new System.NotImplementedException();
    }

    public override void Shoot()
    {
        //throw new System.NotImplementedException();
    }

    public override void ThrowGun()
    {
        //throw new System.NotImplementedException();
    }

    public GameObject icon;

    private void Update()
    {
        GameObject bananaSwordIcon = GameObject.Find("Grap-Pos");

        if (bananaSwordIcon != null)
        {
            Transform bananaSwordTransform = bananaSwordIcon.transform.Find("Grap");

            if (bananaSwordTransform != null && !bananaSwordTransform.gameObject.activeSelf)
            {

                icon.SetActive(false);
            }
            else
            {
                icon.SetActive(true);
            }
        }
    }

    public override bool IsBlocking()
    {
        return state != GunState.None;
    }
}
