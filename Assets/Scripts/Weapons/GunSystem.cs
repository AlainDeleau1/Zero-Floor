using UnityEngine;
using System.Collections;
public abstract class GunSystem : MonoBehaviour
{
    [Header("Stats")]
    public int damage;
    public int bulletsLeft;
    public int bulletsPerTap;
    public int magazineSize;
    public int bulletsShot;
    public float timeBetweenShooting;
    public float range;
    public float reloadTime;
    public float spread;
    public float timeBetweenShots;
    public bool pickedUp;
    public bool readyToShoot;
    public bool allowButtonHold;
    public bool shooting;
    public bool reloading;

    [Header("References")]
    public Player p;
    public new Camera camera;
    public SoundManager sm;
    public PlayerUI ui;
    public Animator anim;
    public LayerMask whatIsEnemy, walls;

    [Header("Particles")]
    public GameObject bloodParticles; 
    public GameObject reloadPrefab;
    public GameObject muzzleEffect;
    public float muzzleEffectDelay;
    public ParticleSystem bulletHolePrefab;

    [Header("Camera Shake")]
    public CameraShake cameraShake;
    public float duration, magnitude;
    protected RaycastHit rayHit; 

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

            if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
            {
                Reload();
            }
        }
        return;
    }

    protected void DecalParticles()
    {
        ParticleSystem spawnedParticles = Instantiate(bulletHolePrefab, rayHit.point, Quaternion.LookRotation(rayHit.normal));
        spawnedParticles.Emit(1);
        Destroy(spawnedParticles.gameObject, 2f);
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

    protected void Update()
    {
        MyInput();
        ui.textoContBalas.text = bulletsLeft.ToString();
    }

    protected IEnumerator MuzzleEffect()
    {
        muzzleEffect.SetActive(true);
        yield return new WaitForSeconds(muzzleEffectDelay);
        muzzleEffect.SetActive(false);
    }

    protected void BloodParticles()
    {
        GameObject newBloodParticles = Instantiate(bloodParticles, rayHit.point, Quaternion.identity);

        if (rayHit.collider.gameObject.CompareTag("Enemy"))
        {
            Transform enemyTransform = rayHit.collider.gameObject.transform;
            newBloodParticles.transform.parent = enemyTransform;
        }
        Destroy(newBloodParticles, 2f);
    }

    public virtual void Reload()
    {
        if (p.died || reloading)
            return;
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }
}

