using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GrenadeExplosion grenade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        print("as;kdn");
        Instantiate(grenade.explosion, grenade.grenadePos.position, grenade.grenadePos.rotation);
    }
}
