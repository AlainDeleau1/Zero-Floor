using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apartment1 : MonoBehaviour
{
    public Animator anim;
    public SensorLevel sl;
    public PlayerUI ui;

    private void OnTriggerStay(Collider other)
    {
        if (sl.departmentOne == false)
        {
            anim.SetBool("Open", true);
            ui.victoryMessage.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("Open", false);
    }
}
