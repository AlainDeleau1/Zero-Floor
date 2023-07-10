using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Animator ani;

    private void OnTriggerStay(Collider other)
    {
        ani.SetBool("OpenAnimation", true);
    }

    private void OnTriggerExit(Collider other)
    {
        ani.SetBool("OpenAnimation", false);
    }
}
