using UnityEngine;

public class MediumEnemyPunch : MonoBehaviour
{
    int damage = 20;
    public Enemy enemy;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponentInParent<Player>();
            if (player != null && enemy.died == false)
            {
                player.TakeDamage(damage);
            }
        }
    }


}

