using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairTarget : MonoBehaviour
{
    public Camera orientation;

    Ray ray;
    RaycastHit hitInfo;    

    void Start()
    {
        orientation = Camera.main;
        if (orientation == null)
        {
            orientation = FindObjectOfType<Camera>();
        }
    }

    void Update()
    {
        ray.origin = orientation.transform.position;
        ray.direction = orientation.transform.forward;
        if (Physics.Raycast(ray, out hitInfo))
        {
            transform.position = hitInfo.point;
        }
    }
}
