using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SensorLevel : MonoBehaviour
{
    public bool departmentOne = false;

    private void OnTriggerStay(Collider other)
    {
        departmentOne = true;
    }

}
