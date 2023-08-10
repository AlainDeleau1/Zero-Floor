using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class KamikazeEnemy : Enemy
{
    public Transform enemyPos;
    public ParticleSystem explosion;
    public Transform kamikazeHand;

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
                if (Vector3.Distance(transform.position, target.transform.position) < attackRange && died == false)
                {
                    Attack();
                    died = true;
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
        if (dying == true || attacking == true)
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

    private async void Attack()
    {
        if (dying)
            return;
        attacking = true;
        ani.SetTrigger("AttackAnim");
        await Task.Delay(600);
        Instantiate(explosion, kamikazeHand.position, kamikazeHand.rotation);
        sm.KamikazeDeathSound();
        Die(0f);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (currentHealth <= 0 && died == false)
        {
            sm.EnemyDeadSound();
            Die(0f);
            Instantiate(explosion, enemyPos.position, enemyPos.rotation);
            sm.KamikazeDeathSound();
            died = true;
        }
    }
}