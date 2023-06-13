using UnityEngine;

public class BulletController : MonoBehaviour
{
    int damage = 20;
    public PlayerUI ui;
    public Player player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            var player = collision.collider.gameObject.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
                ui = FindObjectOfType<PlayerUI>();
                ui.ShowDamage();    
            }
        }
        if (collision.collider.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
