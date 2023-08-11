using System.Threading.Tasks;
using UnityEngine;

public class Pan : GunSystem
{
    public GameObject bulletPrefab;
    public float bulletSpeed;
    public Transform spawnPoint;
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

        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0)); // Dispara desde el centro de la cámara
        RaycastHit hit;

        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();

        if (Physics.Raycast(ray, out hit, range))
        {
            Vector3 targetPoint = hit.point;
            Vector3 shootDirection = (targetPoint - spawnPoint.position).normalized;
            bulletRb.velocity = shootDirection * bulletSpeed;
        }
        else
        {
            bulletRb.velocity = ray.direction * bulletSpeed;
        }

        // Resto de tu lógica, como daño a enemigos, efectos visuales, etc.

        bulletsLeft--;
        Invoke("ResetShot", timeBetweenShooting);
        bulletsShot--;
        if (bulletsShot > 0 && bulletsLeft > 0)
        {
            Invoke("Shoot", timeBetweenShots);
        }

        Destroy(bullet, 2f);
        //sm.RifleShotSound();
    }



    private void Start()
    {
        bulletsLeft = magazineSize;
    }
}

