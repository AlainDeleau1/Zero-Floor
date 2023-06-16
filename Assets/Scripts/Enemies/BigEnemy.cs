using UnityEngine;
using UnityEngine.AI;

public class BigEnemy : Enemy
{
    public float range;
    public Transform centrePoint;
    public Transform enemyMuzzle;
    public GameObject bullet;
    public LayerMask player;
    public Enemy enemy;

    [SerializeField] private int startingHealth = 100;
    [SerializeField] private int damage = 25;
    [SerializeField] private float rotationSpeed = 5f;

    private void Start()
    {
        currentHealth = startingHealth;
    }

    private void Update()
    {
        StalkFunction();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            var player = collision.collider.gameObject.GetComponent<Player>();
            if (player != null && damageReceived == false)
            {
                Debug.Log(damage);
                ui.ShowDamage(2);
                player.TakeDamage(damage);
                damageReceived = true;
                StartCoroutine(AttackDelay());
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void StalkFunction()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
                agent.SetDestination(point);
            }
        }
        else if (Vector3.Distance(transform.position, target.transform.position) < inRange)
        {
            Vector3 targetDirection = target.transform.position - transform.position;
            targetDirection.y = 0f;

            if (targetDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            agent.SetDestination(target.transform.position);
        }
    }
}



