using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCollider : MonoBehaviour
{
    public Pan pan;
    private void OnTriggerEnter(Collider other)
    {
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
