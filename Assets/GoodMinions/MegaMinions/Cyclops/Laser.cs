using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    private void OnCollisionStay(Collision collision)
    {
        // Check if the bullet collides with a purpleMinion
        if (collision.gameObject.CompareTag("Purple"))
        {
            var enemy = collision.gameObject.GetComponent<BaseEnemy>();
            if (enemy)
            {
                enemy.OnHit();
            }

            
        }
    }
}
