using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class Enemy : MonoBehaviour
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
    protected bool dying;
    protected Quaternion angulo;

    public float radius = 1f;
    public float distance = 10f;
    public LayerMask detectionLayer;

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
        RaycastChecksphere();
    }

    public void Die(float deathTime)
    {
        dying = true;
        Destroy(gameObject, deathTime);
        ani.SetTrigger("DeathAnimation");
        gc.killsCounter++;
        gc.kills++;
        int healChance = Random.Range(0, 100);
        if (healChance <= 10)
        {
            Vector3 pillSpawnPosition = agent.transform.position + new Vector3(0f, 1f, 0f);
            GameObject newPill = Instantiate(pill, pillSpawnPosition, Quaternion.identity);
        }
    }

    protected void Patrol()
    {
        if (dying)
            return;
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            SetRandomPatrolPoint();
        }

        agent.SetDestination(patrolPoint);
        agent.speed = patrolSpeed;
        ani.SetBool("PatrolAnimation", true);
    }

    public void SetRandomPatrolPoint()
    {
        if (dying)
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
        if (dying || attacking)
            return;
        agent.SetDestination(target.transform.position);
        agent.speed = chaseSpeed;
    }

    private void RaycastChecksphere()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, radius, transform.forward, distance, detectionLayer);

        foreach (RaycastHit hit in hits)
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.inRange *= 10;
            }
        }
    }
}
