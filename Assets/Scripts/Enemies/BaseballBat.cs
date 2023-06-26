using UnityEngine;

public class BaseballBat : MonoBehaviour
{
    int baseballBatDmg = 25;
    bool damageReceived = false;
    public PlayerUI ui;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponentInParent<Player>();
            if (player != null && damageReceived == false)
            {
                player.TakeDamage(baseballBatDmg);
                damageReceived = true;
                ui.ShowDamage(2);
            }
        }
    }
}

