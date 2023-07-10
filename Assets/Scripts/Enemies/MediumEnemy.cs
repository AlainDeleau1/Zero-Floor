using UnityEngine;
using UnityEngine.AI;

public class MediumEnemy : Enemy
{
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
        ani.SetBool("PatrolAnimation", false);
    }

    private void Attack()
    {
        if (died)
            return;
        StartCoroutine(AttackDelay());
        ani.SetTrigger("AttackMediumEnemy");
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



