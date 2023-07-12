using System.Collections;
using UnityEngine;
using System.Threading.Tasks;

public class Shotgun : GunSystem
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
                        enemy.inRange = enemy.inRange * 10;
                        BloodParticles();                  
                    }
                }
            }
            if (Physics.Raycast(camera.transform.position, directionCone, out rayHit, range, walls))
            {
                DecalParticles();
            }
        }

        bulletsLeft--;
        Invoke("ResetShot", timeBetweenShooting);
        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);
        if (bulletsShot > 0 && bulletsLeft > 0) Invoke("Shoot", timeBetweenShots);

        sm.ShootSound();
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
        bulletsLeft = magazineSize;
    }    

    IEnumerator ReloadPrefab()
    {
        reloadPrefab.SetActive(true);
        yield return new WaitForSeconds(1.2f);
        reloadPrefab.SetActive(false);
    }

    private async void OnEnable()
    {
        sm.ShotgunPickUpSound();
        await Task.Delay(1500);
        pickedUp = false;
    }

    public override void Reload()
    {
        base.Reload();
        if (!reloading)
        {
            StartCoroutine(ReloadPrefab());
            sm.ReloadSound();
            reloading = true;
        }
    }
}
