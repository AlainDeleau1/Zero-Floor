using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    public Player p;
    public GameObject pill;


    private void OnTriggerEnter(Collider other)
    {
        if (p.currentHealth < 100)
        {
            TakePill();
        }
    }
    
    private void TakePill()
    {
        p.TakeDamage(p.currentHealth - 100);
        Destroy(pill, 0f);
    }
}