using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public abstract class BaseGun : MonoBehaviour
{
    

    public BaseGun()
    {
        Ammo = MaxAmmo;
    }

    public abstract void ThrowGun();

    public abstract void Reload(bool instant);

    public abstract void Shoot();

    public abstract bool IsBlocking();

    public int MaxAmmo = 25;
    public int Ammo { get; protected set; }
}
