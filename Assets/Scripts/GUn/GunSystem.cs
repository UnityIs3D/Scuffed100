using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GunSystem : MonoBehaviour
{
    public List<BaseGun> guns;
    private BaseGun current;
    

    private void Start()
    {
        EquipSlot(0);
    }

    private void Update()
    {
        if (current)
        {
            if (Input.GetButton("Fire1"))
            {
                current.Shoot();
            }

            //if (Input.GetKey(KeyCode.R))
            //{
            //    current.Reload(false);
            //}
        }

        if (!current.IsBlocking())
        {
            for (int i = 0; i < guns.Count; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString())) // "1" key corresponds to slots[0], "2" to slots[1], etc.
                {
                    EquipSlot(i);
                }
            }
        }

    }

    void EquipSlot(int slotIndex)
    {
        current = guns[slotIndex];
        for (int i = 0; i < guns.Count; i++)
        {
            guns[i].transform.GetChild(0).gameObject.SetActive(i == slotIndex);
        }
    }
}