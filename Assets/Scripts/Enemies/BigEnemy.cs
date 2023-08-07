using UnityEngine;
using UnityEngine.AI;

public class BigEnemy : Enemy
{
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
                if (Vector3.Distance(transform.position, target.transform.position) < 2f && died == false)
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
        if (dying)
        {
            return;
        }
        base.ChasePlayer();
        Vector3 targetDirection = target.transform.position - transform.position;
        targetDirection.y = 0f;
        targetDirection.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        if (dying)
            return;
        StartCoroutine(AttackDelay());
        ani.SetTrigger("AttackAnimation");
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
