using System.Collections;
using UnityEngine;

public class Rifle : GunSystem
{
    [Header("Gun stats")]
    [SerializeField] public int magazineSize, bulletsPerTap, damage;
    [SerializeField] public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots, shotForce;

    [Header("Sounds & Visuals")]
    [SerializeField] private CameraShake cameraShake;
    [SerializeField] private float duration, magnitude;
    [SerializeField] private GameObject particlesEffect, bloodParticles;
    [SerializeField] private ParticleSystem bulletHolePrefab;
    [SerializeField] private SoundManager sm;
    [SerializeField] private PlayerUI ui;

    private Animator anim;
    public bool riflePickedUpLoaded = false;

    public HolesPool hp;

    public override void MyInput()
    {
        base.MyInput();
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading && p.died == false)
        {
            Reload();
        }

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0 && p.died == false)
        {
            Shoot();

            bulletsShot = bulletsPerTap;
        }
        else if (readyToShoot && shooting && !reloading && bulletsLeft <= 0 && p.died == false)
        {
            sm.OutOfAmmoSound();
        }
    }

    public void Shoot()
    {
        readyToShoot = false;

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out rayHit, range, whatIsEnemy))
        {

            if (rayHit.collider.gameObject.CompareTag("Enemy"))
            {
                var enemy = rayHit.collider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    sm.EnemyDamagedSound();
                    enemy.TakeDamage(damage);
                    //BloodParticles();
                }
            }
            //if (rayHit.collider.gameObject.CompareTag("Walls"))
            //{
            //    
            //}
        }
    
        bulletsLeft--;
        Invoke("ResetShot", timeBetweenShooting);

        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0) Invoke("Shoot", timeBetweenShots);

        StartCoroutine(ParticleView());

        anim.SetTrigger("shootRifleAni");

        sm.RifleShotSound();

        StartCoroutine(cameraShake.Shake(duration, magnitude));
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        bulletsLeft = magazineSize;
        particlesEffect.SetActive(false);
        allowButtonHold = true;
    }

    private void OnEnable()
    {
        riflePickedUpLoaded = false;
    }

    private void Update()
    {
        MyInput();
        ui.textoContBalas.text = bulletsLeft.ToString();
        if (enabled == true && riflePickedUpLoaded == false)
        {
            sm.RiflePickUpSound();
            riflePickedUpLoaded = true;
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    IEnumerator ParticleView()
    {
        particlesEffect.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        particlesEffect.SetActive(false);
    }

    private void BloodParticles()
    {
        GameObject newBloodParticles = Instantiate(bloodParticles, rayHit.point, Quaternion.identity);
        Instantiate(newBloodParticles, rayHit.point, Quaternion.identity);
        Destroy(newBloodParticles, 2f);
    }

    private void Reload()
    {
        reloading = true;
        sm.ReloadSound();
        Invoke("ReloadFinished", reloadTime);
    }

    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;
        reloading = false;
    }
}

