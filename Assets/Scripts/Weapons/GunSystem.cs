using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public abstract class GunSystem : MonoBehaviour
{
    public int bulletsLeft;
    public bool pickedUp = false;
    public bool readyToShoot;

    [Header("References")]
    [SerializeField] protected new Camera camera;
    [SerializeField] protected LayerMask whatIsEnemy, walls;
    public Player p;

    protected RaycastHit rayHit;
    protected bool allowButtonHold;
    protected bool shooting, reloading;
    protected int bulletsShot;

    public virtual void MyInput()
    {
        if (p.died == false)
        {
            if (allowButtonHold)
            {
                shooting = Input.GetKey(KeyCode.Mouse0);
            }

            else
            {
                shooting = Input.GetKeyDown(KeyCode.Mouse0);
            }
        }
    }
}

