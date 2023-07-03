using UnityEngine;

public class BulletController : MonoBehaviour
{
    int damage = 10;
    public PlayerUI ui;
    public Player player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var player = other.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
                ui = FindObjectOfType<PlayerUI>();
                ui.ShowDamage(2);
                //Destroy(gameObject, 2f);
            }
        }

    }
}
