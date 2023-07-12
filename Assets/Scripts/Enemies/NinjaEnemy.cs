using UnityEngine;
using UnityEngine.AI;

public class NinjaEnemy : Enemy
{
    private Vector3 startingPosition;
    private bool canTeleport = true;
    public float teleportRadius = 5f;

    private void Start()
    {
        currentHealth = startingHealth;
        SetRandomPatrolPoint();
        startingPosition = transform.position;
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
                if (Vector3.Distance(transform.position, target.transform.position) < attackRange && died == false)
                {
                    Attack();
                }
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
        base.ChasePlayer();
        Vector3 targetDirection = target.transform.position - transform.position;
        targetDirection.y = 0f;
        targetDirection.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        if (died || attacking)
            return;
        attacking = true;
        ani.SetTrigger("AttackAnimation");
        StartCoroutine(AttackDelay());
        Invoke("Teleport", attackDelay);
        
    }

    private void Teleport()
    {
        if (canTeleport)
        {
            attacking = false;
            Vector3 randomPoint = startingPosition + Random.insideUnitSphere * teleportRadius;
            NavMeshHit hit;

            if (NavMesh.SamplePosition(randomPoint, out hit, teleportRadius, NavMesh.AllAreas))
            {
                agent.Warp(hit.position);
            }

            canTeleport = false;
            Invoke("ResetTeleport", .2f);
        }
    }

    private void ResetTeleport()
    {
        canTeleport = true;
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
