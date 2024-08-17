using UnityEngine;

public class TurningYellow : MonoBehaviour
{
    

    private void OnCollisionEnter(Collision collision)
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

