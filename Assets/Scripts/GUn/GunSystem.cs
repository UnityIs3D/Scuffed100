using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GunSystem : MonoBehaviour
{
    public List<BaseGun> guns;
    private BaseGun current;
    private int currentWeaponIndex = 0;  // Track the current weapon index

    private void Start()
    {
        EquipSlot(0);  // Equip the first weapon by default
    }

    private void Update()
    {
        if (current)
        {
            if (Input.GetButton("Fire1"))
            {
                current.Shoot();
            }

            // Uncomment and implement Reload if necessary
            //if (Input.GetKey(KeyCode.R))
            //{
            //    current.Reload(false);
            //}
        }

        if (!current.IsBlocking())
        {
            // Handle mouse scroll wheel input
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll != 0)
            {
                int direction = scroll > 0 ? 1 : -1;  // Scroll up (positive) or down (negative)
                int newIndex = currentWeaponIndex + direction;

                // Wrap around the weapon list if needed
                newIndex = (newIndex + guns.Count) % guns.Count;

                EquipSlot(newIndex);  // Equip the weapon at the new index
                currentWeaponIndex = newIndex;  // Update the current weapon index
            }

            // Handle number key inputs for explicit slot selection
            for (int i = 0; i < guns.Count; i++)
            {
                if (Input.GetKeyDown((i + 1).ToString()))  // "1" key corresponds to slots[0], "2" to slots[1], etc.
                {
                    EquipSlot(i);
                    currentWeaponIndex = i;  // Update the current weapon index
                }
            }
        }
    }

    void EquipSlot(int slotIndex)
    {
        current = guns[slotIndex];  // Set the current weapon to the one at the specified slot index
        for (int i = 0; i < guns.Count; i++)
        {
            // Only activate the child object corresponding to the selected weapon
            guns[i].transform.GetChild(0).gameObject.SetActive(i == slotIndex);
        }
    }
}
