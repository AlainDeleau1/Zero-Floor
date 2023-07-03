using System.Collections;
using UnityEngine;
using System.Threading.Tasks;

public class Shotgun : GunSystem
{
    [Header("Gun stats")]
    public int damage;
    public float spread, timeBetweenShots;
    
    [Header("Sounds & Visuals")]
    [SerializeField] CameraShake cameraShake;
    
    [SerializeField] private GameObject particlesEffect, reloadPrefab;
    [SerializeField] private ParticleSystem bulletHolePrefab;

    private Animator anim;
    public SoundManager sm;
    public PlayerUI ui;

    public bool shotgunPickedUpLoaded = false;

    public override void MyInput()
    {
        base.MyInput();
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        if (shooting)
            Shoot();

        if (shooting && bulletsLeft <= 0 && !reloading)
        {
            sm.OutOfAmmoSound();
            Invoke("Reload", .5f);
        }     
    }

    public void Shoot()
    {
        if (p.died || !readyToShoot || reloading || bulletsLeft <= 0)
        {
            return;     
        }

        readyToShoot = false;

        for (int i = 0; i < bulletsPerTap; i++)
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
                        BloodParticles();                  
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

    private async void OnEnable()
    {
        shotgunPickedUpLoaded = false;
        await Task.Delay(1500);
        pickedUp = false;
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
        if (p.died || reloading)
            return;      
        sm.ReloadSound();
        StartCoroutine(ReloadPrefab());
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
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
}
