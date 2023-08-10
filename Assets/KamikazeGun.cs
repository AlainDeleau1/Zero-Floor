using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeGun : MonoBehaviour
{
    public int damage = 35;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            var player = other.GetComponent<Player>();
            if (player)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
