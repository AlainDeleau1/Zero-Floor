using UnityEngine;

public class MediumEnemyPunch : MonoBehaviour
{
    public int damage;
    public Enemy enemy;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponentInParent<Player>();
            if (player != null && enemy.died == false)
            {
                print("damage");
                player.TakeDamage(damage);
            }
        }
    }


}

