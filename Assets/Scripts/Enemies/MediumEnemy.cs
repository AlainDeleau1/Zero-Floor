using UnityEngine;
using UnityEngine.AI;

public class MediumEnemy : Enemy
{
    public float patrolRadius = 10f;
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;

    public Animator ani;

    [SerializeField] private Vector3 patrolPoint;
    private bool isPatrolling;
    private bool isChasing;

    [SerializeField] private int startingHealth = 100;
    [SerializeField] private float rotationSpeed = 5f;

    public SensorLevel sl;


    private void Start()
    {
        currentHealth = startingHealth;
        SetRandomPatrolPoint();
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, target.transform.position) < inRange && sl.departmentOne == true)
        {
            isChasing = true;
            isPatrolling = false;
            if (isChasing)
            {
                //print("Chase");
                ChasePlayer();
                if (Vector3.Distance(transform.position, target.transform.position) < 2f)
                {
                    //print("Attack");
                    Attack();
                }
            }
        }
        else
        {
            isPatrolling = true;
            isChasing = false;
            //print("Patrol");
            Patrol();
        }
    }

    private void Patrol()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            SetRandomPatrolPoint();
        }

        agent.SetDestination(patrolPoint);
        agent.speed = patrolSpeed;
    }

    private void SetRandomPatrolPoint()
    {
        Vector3 randomPoint = transform.position + Random.insideUnitSphere * patrolRadius;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, patrolRadius, NavMesh.AllAreas))
        {
            patrolPoint = hit.position;
        }
    }

    private void ChasePlayer()
    {
        transform.LookAt(target.transform);

        agent.SetDestination(target.transform.position);
        agent.speed = chaseSpeed;

        Vector3 targetDirection = target.transform.position - transform.position;
        targetDirection.y = 0f;
        targetDirection.Normalize();

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private void Attack()
    {
        ani.SetTrigger("BaseballAttack");
    }
}



