using UnityEngine;
using System.Threading.Tasks;
public class BaseballBat : MonoBehaviour
{
    int baseballBatDmg = 20;
    bool damageReceived = false;
    public PlayerUI ui;

    public async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponentInParent<Player>();
            if (player != null)
            {
                player.TakeDamage(baseballBatDmg);
                ui.ShowDamage(2);
                await Task.Delay(100);
            }
        }
    }
}

