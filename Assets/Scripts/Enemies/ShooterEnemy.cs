using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ShooterEnemy : Enemy
{
    public LayerMask layerPlayer;
    public Transform muzzleEnemyGun;
    public GameObject bulletEnemyPrefab;
    public GameObject muzzleEffect;
    int bulletEnemyForce = 6000;

    private void Start()
    {
        currentHealth = startingHealth;
        SetRandomPatrolPoint();
    }

    private void Update()
    {
        if (dying)
            agent.GetComponent<NavMeshAgent>().enabled = false;

        if (Vector3.Distance(transform.position, target.transform.position) < inRange)
        {
            isChasing = true;
            isPatrolling = false;
            if (isChasing)
            {
                ChasePlayer();
            }
        }
        else
        {
            isPatrolling = true;
            isChasing = false;
            Patrol();
        }
    }

    public override void ChasePlayer()
    {
        if (dying == true)
        {
            return;
        }
        base.ChasePlayer();
        Vector3 targetDirection = target.transform.position - transform.position;
        targetDirection.y = 0f;
        targetDirection.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        RaycastHit hit;
        if (Physics.Raycast(muzzleEnemyGun.transform.position, targetDirection, out hit, inRange, layerPlayer))
        {
            Debug.DrawRay(muzzleEnemyGun.transform.position, targetDirection * hit.distance, Color.yellow);
            if(hit.collider.gameObject.CompareTag("Player"))
            {             
                var player = hit.collider.gameObject.GetComponent<Player>();
                if (player != null && damageReceived == false)
                {
                    FireProjectile();
                    damageReceived = true;
                    StartCoroutine(AttackDelay());
                }
            }
        }
        else
        {
            Debug.DrawRay(muzzleEnemyGun.transform.position, targetDirection * inRange, Color.red);
        }
    }

    private void FireProjectile()
    {
        if (dying)
            return;

        GameObject newProjectile = Instantiate(bulletEnemyPrefab, muzzleEnemyGun.position, muzzleEnemyGun.rotation);
        Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>();
        projectileRigidbody.AddForce(muzzleEnemyGun.forward * bulletEnemyForce);
        Destroy(newProjectile, 2f);
        StartCoroutine(MuzzleEffect());
    }

    private IEnumerator MuzzleEffect()
    {
        muzzleEffect.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        muzzleEffect.SetActive(false);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (currentHealth <= 0 && died == false)
        {

            sm.EnemyDeadSound();
            Die(2f);
            died = true;
        }
    }
}

