using UnityEngine;
using System.Collections;

public class TurretBehaviour : MonoBehaviour
{
    public Transform target;
    public Transform pivotTurret;
    public float rotationSpeed = 5f;
    public float range = 10f;
    public Transform muzzleTurret;
    public GameObject projectile;
    public float projectileForce = 200f;
    public float projectileDelay = 0.3f;

    private float lastProjectileTime;
    private Quaternion initialRotation;

    public GameObject MuzzleFlashEffect;

    private void Start()
    {
        lastProjectileTime = -projectileDelay;
        initialRotation = pivotTurret.rotation;
    }

    private void Update()
    {
        Vector3 targetDirection = target.position - pivotTurret.position;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Limitar el rango de movimiento a 30 grados
        Quaternion clampedRotation = ClampRotation(targetRotation);

        pivotTurret.rotation = Quaternion.Slerp(pivotTurret.rotation, clampedRotation, rotationSpeed);

        RaycastHit hit;
        if (Physics.Raycast(muzzleTurret.position, muzzleTurret.forward, out hit, range))
        {
            if (hit.collider.CompareTag("Player"))
            {
                float currentTime = Time.time;
                if (currentTime - lastProjectileTime >= projectileDelay)
                {
                    FireProjectile();
                    lastProjectileTime = currentTime;
                }
            }
        }
    }

    private void FireProjectile()
    {
        GameObject newProjectile = Instantiate(projectile, muzzleTurret.position, muzzleTurret.rotation);
        Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>();
        projectileRigidbody.AddForce(muzzleTurret.forward * projectileForce);
        StartCoroutine(ParticleView());
    }

    private IEnumerator ParticleView()
    {
        MuzzleFlashEffect.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        MuzzleFlashEffect.SetActive(false);
    }

    private Quaternion ClampRotation(Quaternion targetRotation)
    {
        // Limitar el rango de rotación a 30 grados
        Quaternion clampedRotation = targetRotation * Quaternion.Inverse(initialRotation);
        clampedRotation.eulerAngles = new Vector3(0f, Mathf.Clamp(clampedRotation.eulerAngles.y, -15f, 15f), 0f);
        return initialRotation * clampedRotation;
    }
}










