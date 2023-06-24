using System.Collections;
using UnityEngine;

public class Shotgun : GunSystem
{
    [Header("Gun stats")]
    [SerializeField] public int magazineSize, bulletsPerTap, damage;
    [SerializeField] public float timeBetweenShooting, spread, range, reloadTime, timeBetweenShots, shotForce;
    
    [Header("Sounds & Visuals")]
    [SerializeField] CameraShake cameraShake;
    [SerializeField] private float duration, magnitude;
    [SerializeField] private GameObject particlesEffect, bloodParticles, reloadPrefab;
    [SerializeField] private ParticleSystem bulletHolePrefab;

    private Animator anim;
    public SoundManager sm;
    public PlayerUI ui;

    public bool shotgunPickedUpLoaded = false;

    public override void MyInput()
    {
        base.MyInput();
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading && p.died == false)
        {
            Reload();
            StartCoroutine(ReloadPrefab());
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
        print("Dispare shotgun");
        readyToShoot = false;

        for (int i = 0; i < 20; i++)
        {
            Vector3 directionCone = GetConeDirection(12f);
            if (Physics.Raycast(camera.transform.position, directionCone, out rayHit, range, whatIsEnemy))
            {
                if (rayHit.collider.gameObject.CompareTag("Enemy"))
                {
                    var enemy = rayHit.collider.gameObject.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        sm.EnemyDamagedSound();
                        enemy.TakeDamage(damage);
                        GameObject newBlood = Instantiate(bloodParticles, rayHit.point, Quaternion.identity);
                        Destroy(newBlood, 1f);
                        
                    }
                }
            }
            else if (Physics.Raycast(camera.transform.position, directionCone, out rayHit, range, walls))
            {
                ParticleSystem spawnedParticles = Instantiate(bulletHolePrefab, rayHit.point, Quaternion.LookRotation(rayHit.normal));
                spawnedParticles.Emit(1);
                Destroy(spawnedParticles.gameObject, 2f);
            }
        }

        bulletsLeft--;
        Invoke("ResetShot", timeBetweenShooting);

        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0) Invoke("Shoot", timeBetweenShots);

        StartCoroutine(ParticleView());

        anim.SetTrigger("ShootAnim");

        sm.ShootSound();

        StartCoroutine(cameraShake.Shake(duration, magnitude));
    }

    private Vector3 GetConeDirection(float coneAngle)
    {
        Vector3 direction = camera.transform.forward;

        float randomAngleX = Random.Range(-coneAngle, coneAngle);
        float randomAngleY = Random.Range(-coneAngle, coneAngle);

        Quaternion coneRotation = Quaternion.Euler(randomAngleY, randomAngleX, 0);
        direction = coneRotation * direction;

        return direction;
    }




    private void Start()
    {
        anim = GetComponent<Animator>();
        bulletsLeft = magazineSize;
        particlesEffect.SetActive(false);      
    }

    private void OnEnable()
    {
        shotgunPickedUpLoaded = false;
    }

    private void Update()
    {
        MyInput();
        ui.textoContBalas.text = bulletsLeft.ToString();
        if (enabled == true && shotgunPickedUpLoaded == false)
        {
            sm.PickedWeaponSound();
            shotgunPickedUpLoaded = true;
        }
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    IEnumerator ParticleView()
    {
        particlesEffect.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        particlesEffect.SetActive(false);
    }

    IEnumerator ReloadPrefab()
    {
        reloadPrefab.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        reloadPrefab.SetActive(false);
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
