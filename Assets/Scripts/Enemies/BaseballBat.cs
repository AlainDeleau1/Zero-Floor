using UnityEngine;
using System.Threading.Tasks;

public class BaseballBat : MonoBehaviour
{
    int baseballBatDmg = 20;
    public PlayerUI ui;
    public Enemy enemy;

    public async void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            var player = other.GetComponentInParent<Player>();
            if (player != null && enemy.died == false)
            {
                player.TakeDamage(baseballBatDmg);
                ui.ShowDamage(2);
                await Task.Delay(0);
            }
        }
    }


}

