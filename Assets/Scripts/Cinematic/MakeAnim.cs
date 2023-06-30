using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeAnim : MonoBehaviour
{
    public Animator myAnim;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("abrio");
        if (other.CompareTag ("CameraRef"))
        {
            Debug.Log("abrio");
            myAnim.SetBool("Open", true);
        }   
    }
    
}
