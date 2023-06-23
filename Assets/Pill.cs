using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pill : MonoBehaviour
{
    public Player p;
    public Camera playerCam;
    public float range;
    public GameObject currentWeapon;
    public GameObject weapon;
    public PlayerUI ui;
    public bool canGrab;

    private void Update()
    {
        CheckWeapons();
        if (canGrab)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Pickup();
            }
        }   
    }
    private void CheckWeapons()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hit, range))
        {
            if (hit.transform.tag == "Healing")
            {
                ui.interactText.enabled = true;
                canGrab = true;
                weapon = hit.transform.gameObject;
            }
        }
        else
        {
            canGrab = false;
            ui.interactText.enabled = false;
        }
    }
    private void Pickup()
    {
        currentWeapon = weapon;
        p.TakeDamage(p.currentHealth - 100);
        Destroy(currentWeapon, 0f);
    }
}