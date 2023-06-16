using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class BaseballBat : MonoBehaviour
{
    int baseballBatDmg = 25;
    bool damageReceived = false;
    public PlayerUI ui;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.collider.gameObject.name);
            var player = collision.collider.gameObject.GetComponentInParent<Player>();
            if (player != null && damageReceived == false)
            {
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
