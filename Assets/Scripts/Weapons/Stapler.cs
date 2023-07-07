using System.Threading.Tasks;
using UnityEngine;

public class Stapler : GunSystem
{
    public override void MyInput()
    {
        base.MyInput();

        if (shooting)
        {
            Shoot();
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
        StartCoroutine(MuzzleEffect());
        StartCoroutine(cameraShake.Shake(duration, magnitude));
        anim.SetTrigger("ShootAnim");

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
            DecalParticles();
        }

        bulletsLeft--;
        Invoke("ResetShot", timeBetweenShooting);
        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);
        if (bulletsShot > 0 && bulletsLeft > 0) Invoke("Shoot", timeBetweenShots);

        sm.RifleShotSound();
    }

    private void Start()
    {
        bulletsLeft = magazineSize;
    }

    private async void OnEnable()
    {
        //Sonido de pick up
        await Task.Delay(1500);
        pickedUp = false;
    }

    public override void Reload()
    {
        base.Reload();
        sm.RifleReloadSound();
    }
}