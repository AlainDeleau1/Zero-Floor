using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ShooterEnemy : Enemy
{
    public float patrolRadius = 10f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    [SerializeField] private Vector3 patrolPoint;
    private bool isPatrolling;
    private bool isChasing;

    [SerializeField] private int startingHealth = 100;
    [SerializeField] private float rotationSpeed = 1f;

    public LayerMask layerPlayer;

    public Transform muzzleEnemyGun;

    public GameObject bulletEnemyPrefab;

    public GameObject muzzleEffect;

    int bulletEnemyForce = 6000;

    public Animator ani;

    private void Start()
    {
        currentHealth = startingHealth;
        SetRandomPatrolPoint();
    }

    private void Update()
    {
        if (died)
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

    private void Patrol()
    {
        if (died)
            return;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            SetRandomPatrolPoint();
        }

        agent.SetDestination(patrolPoint);
        agent.speed = patrolSpeed;
    }

    private void SetRandomPatrolPoint()
    {
        if (died)
            return;
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * patrolRadius;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, NavMesh.AllAreas))
        {
            patrolPoint = hit.position;
        }
    }

    private void ChasePlayer()
    {
        if (died)
            return;
             
        transform.LookAt(target.transform);

        agent.SetDestination(target.transform.position);
        agent.speed = chaseSpeed;

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
                    print("dispara enemy");
                    damageReceived = true;
                    StartCoroutine(newAttackDelay());
                }
            }
        }
        else
        {
            Debug.DrawRay(muzzleEnemyGun.transform.position, targetDirection * inRange, Color.red);
        }
    }

    private IEnumerator newAttackDelay()
    {
        yield return new WaitForSeconds(.35f);
        damageReceived = false;
    }

    private void FireProjectile()
    {
        if (died)
            return;

        GameObject newProjectile = Instantiate(bulletEnemyPrefab, muzzleEnemyGun.position, muzzleEnemyGun.rotation);
        Rigidbody projectileRigidbody = newProjectile.GetComponent<Rigidbody>();
        projectileRigidbody.AddForce(muzzleEnemyGun.forward * bulletEnemyForce);
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
            ani.SetTrigger("DeathAnimation");
            sm.EnemyDeadSound();
            Die(2f);
            died = true;
        }
    }
}

