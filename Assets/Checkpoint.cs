using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public Vector3 vectorPoint;
    public GameObject player;
    public Player p;
    public bool passed = false;

    private void OnTriggerEnter(Collider other)
    {
        print("saldibsdjlah");
        vectorPoint = player.transform.position;
        passed = true;
    }

    private void Update()
    {
        if (p.died == false)
        {
            player.transform.position = vectorPoint;
        }
    }

}
