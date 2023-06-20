using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballBat : MonoBehaviour
{
    int baseballBatDmg = 25;
    bool damageReceived = false;
    public PlayerUI ui;
    public Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            var player = collision.collider.gameObject.GetComponentInParent<Player>();
            if (player != null)
            {
                rb.AddForce(-transform.forward * 9999f, ForceMode.Impulse);
                player.TakeDamage(baseballBatDmg);
                damageReceived = true;
                ui.ShowDamage(2);
                StartCoroutine(AttackDelay());
            }

        }
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(2f);
        damageReceived = false;
    }
}

