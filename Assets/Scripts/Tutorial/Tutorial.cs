using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject movement, dash, jump, shoot, drop;

    private void Update()
    {

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            if (movement.activeInHierarchy == false)
            {
                return;
            }
            else
            {
                movement.SetActive(false);
                dash.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            if (dash.activeInHierarchy == false)
            {
                return;
            }
            dash.SetActive(false);
            jump.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (jump.activeInHierarchy == false)
            {
                return;
            }
            jump.SetActive(false);
            shoot.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (shoot.activeInHierarchy == false)
            {
                return;
            }
            shoot.SetActive(false);
            drop.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if (drop.activeInHierarchy == false)
            {
                return;
            }
            drop.SetActive(false);
            
        }
    }
}
