using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolesPool : MonoBehaviour
{
    public static HolesPool instance;
    public GameObject prefabHole;
    void Awake()
    {
        instance = this;
    }

    public void Impact(Vector3 position, Vector3 normal)
    {
        var hole = Instantiate(prefabHole, position, transform.rotation);
        hole.transform.forward = normal;
    }
}
