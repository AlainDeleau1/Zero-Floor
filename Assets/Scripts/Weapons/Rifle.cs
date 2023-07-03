using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class Rifle : GunSystem
{
    [Header("Gun stats")]
    public int damage;
    public float spread, timeBetweenShots;

    [Header("Sounds & Visuals")]
    [SerializeField] private CameraShake cameraShake;

    [SerializeField] private GameObject particlesEffect;
    [SerializeField] private ParticleSystem bulletHolePrefab;
    [SerializeField] private SoundManager sm;
    [SerializeField] private PlayerUI ui;

    private Animator anim;
    public bool riflePickedUpLoaded = false;

    public override void MyInput()
    {
        base.MyInput();
        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < magazineSize && !reloading)
        {
            Reload();
        }

        if (shooting)
        {
            Shoot();
            bulletsShot = bulletsPerTap;
        }

        if (shooting && bulletsLeft <= 0 && reloading == false)
        {
            sm.OutOfAmmoSound();
            Invoke("Reload", .5f);
        }
    }

    public void Shoot()
    {
        if (p.died || !readyToShoot || reloading || bulletsLeft <= 0)              
            return;
        
           
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
                    BloodParticles();
                }
            }
        }

        if (Physics.Raycast(camera.transform.position, camera.transform.forward, out rayHit, range, walls))
        {
            ParticleSystem spawnedParticles = Instantiate(bulletHolePrefab, rayHit.point, Quaternion.LookRotation(rayHit.normal));
            spawnedParticles.Emit(1);
            Destroy(spawnedParticles.gameObject, 2f);
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

    private async void OnEnable()
    {
        riflePickedUpLoaded = false;
        await Task.Delay(1500);
        pickedUp = false;
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

    IEnumerator ParticleView()
    {
        particlesEffect.SetActive(true);
        yield return new WaitForSeconds(0.05f);
        particlesEffect.SetActive(false);
    }

    private void Reload()
    {
        if (p.died || reloading)
            return;
        sm.RifleReloadSound();
        reloading = true;
        Invoke("ReloadFinished", reloadTime);
    }

    private void BloodParticles()
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

