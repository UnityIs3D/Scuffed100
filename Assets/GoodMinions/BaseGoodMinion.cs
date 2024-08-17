using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGoodMinion : MonoBehaviour
{
    private  float goodGuyHealth;

    private void Start()
    {
        goodGuyHealth = MinionHealth();

    }

    public void OnHit()
    {
        goodGuyHealth -= 1;
        if (goodGuyHealth <= 0)
        {
            DieGoodMinion();
        }
        else
        {
            HitGoodMinion();
        }
    }


    protected virtual int MinionHealth()
    {
        return 0;
    }


    protected virtual void HitGoodMinion()
    {
            //
    }

    protected virtual void DieGoodMinion()
    {
        throw new System.NotImplementedException();
    }

    
}
