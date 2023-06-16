using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    public GameObject pillPrefab;
    public int healingValue = -50;
    bool healed = false;
    public Player p;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player") && healed == false && p.currentHealth <= 50)
        {
            var player = collision.collider.gameObject.GetComponentInParent<Player>();
            player.TakeDamage(healingValue);
            healed = true;
            Destroy(pillPrefab);
        }
    }
}
