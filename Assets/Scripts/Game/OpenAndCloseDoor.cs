using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndCloseDoor: MonoBehaviour
{
    public Animator anim;
    public PlayerUI ui;

    private void OnTriggerStay(Collider other)
    {
        anim.SetBool("Open", true);
        ui.victoryMessage.gameObject.SetActive(false);      
    }

    private void OnTriggerExit(Collider other)
    {
        anim.SetBool("Open", false);
    }
}
