using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    public int startingHealth = 100;
    public float rotationSpeed = 1f;
    public float patrolRadius = 10f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float attackDelay;
    public float inRange;
    public float attackRange;
    public bool died = false;
    protected bool isPatrolling;
    protected bool isChasing;
    protected Vector3 patrolPoint;
    protected int currentHealth;
    protected bool damageReceived = false;
    protected bool attacking;
    protected Quaternion angulo;

    [Header("References")]
    [SerializeField] protected GameObject target, pill;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected SoundManager sm;
    [SerializeField] protected GameController gc;
    public Animator ani;

    protected IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackDelay);
        damageReceived = false;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    public void Die(float deathTime)
    {
        Destroy(gameObject, deathTime);
        gc.killsCounter++;
        gc.kills++;
        int healChance = Random.Range(0, 100);
        if (healChance <= 10)
        {
            Vector3 pillSpawnPosition = agent.transform.position + new Vector3(0f, 1f, 0f);
            GameObject newPill = Instantiate(pill, pillSpawnPosition, Quaternion.identity);
            Destroy(newPill, 10f);
        }
    }

    protected void Patrol()
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

    public void SetRandomPatrolPoint()
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

    public virtual void ChasePlayer()
    {
        if (died || attacking)
            return;
        agent.SetDestination(target.transform.position);
        agent.speed = chaseSpeed;
    }
}
