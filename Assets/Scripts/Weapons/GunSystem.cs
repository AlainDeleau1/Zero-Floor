using UnityEngine;
public abstract class GunSystem : MonoBehaviour
{
    public bool pickedUp = false;
    public bool readyToShoot;
    public int bulletsLeft, bulletsPerTap;
    public float timeBetweenShooting;
    public float range;
    public float reloadTime;
    public float duration;
    public float magnitude;

    [Header("References")]
    [SerializeField] protected new Camera camera;
    [SerializeField] protected LayerMask whatIsEnemy, walls;
    public Player p;
    public GameObject bloodParticles;

    protected RaycastHit rayHit;
    protected bool allowButtonHold;
    protected bool shooting, reloading;
    protected int bulletsShot;
    public int magazineSize;

    public virtual void MyInput()
    {
        if (p.died)
        {
            return;
        }

        if (allowButtonHold)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }
    }


    protected void ResetShot()
    {
        readyToShoot = true;
    }

    protected void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}

