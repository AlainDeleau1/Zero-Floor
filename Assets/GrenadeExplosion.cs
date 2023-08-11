using UnityEngine;

public class GrenadeExplosion : GrenadeReadyToShoot
{
    public int damage;
    public ParticleSystem explosion;
    public Transform grenadePos;
    public GameObject grenadePrefab;
    public Transform spawnPoint;
    public float throwForce;
    public Rigidbody rb;
    public bool coll = false;
    public int timeGrenade;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && readyToShoot == true)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.transform.SetParent(null);
            ThrowGrenade();
            readyToShoot = false;
        }
    }

    void ThrowGrenade()
    {
        readyToShoot = false;       
        GameObject newGrenade = Instantiate(grenadePrefab, spawnPoint.position, spawnPoint.rotation);
        Rigidbody grenadeRb = newGrenade.GetComponent<Rigidbody>();
        grenadeRb.AddForce(spawnPoint.forward * throwForce, ForceMode.Impulse);
        Destroy(newGrenade, 1.5f);
        Invoke("ResetGrenade", timeGrenade);
    }

    private void ResetGrenade()
    {
        readyToShoot = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<Enemy>())
        {
            var enemy = other.GetComponent<Enemy>();
            if (enemy)
            {
                enemy.TakeDamage(damage);
            }
            if (coll == false)
            {
                Instantiate(explosion, grenadePos.position, grenadePos.rotation);
                coll = true;
            }
        }
    }
}
