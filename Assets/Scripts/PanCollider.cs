using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCollider : MonoBehaviour
{
    public Pan pan;

    private void OnTriggerEnter(Collider other)
    {
        if (pan.readyToAttack == false)
        {
            print("pan ready to attack");
            pan.panCollider.enabled = true;
            if (other.GetComponent<Enemy>())
            {
                var enemy = other.GetComponent<Enemy>();
                if (enemy)
                {
                    print("dmggg");
                    enemy.TakeDamage(pan.damage);
                }
            }
        }      
    }
    private void OnTriggerExit(Collider other)
    {
        if (pan.readyToAttack == false)
        {
            print("pan ready to attack");
            pan.panCollider.enabled = true;
            if (other.GetComponent<Enemy>())
            {
                var enemy = other.GetComponent<Enemy>();
                if (enemy)
                {
                    print("dmggg");
                    enemy.TakeDamage(pan.damage);
                }
            }
        }
    }
}
