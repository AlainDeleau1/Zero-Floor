using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FastEnemy : Enemy
{
    [SerializeField] private int startingHealth = 100;
    [SerializeField] private int damage = 25;

    public float range;

    public Transform centrePoint;

    private void Start()
    {
        currentHealth = startingHealth;
    }
    private void Awake()
    {
        ui = FindObjectOfType<PlayerUI>();
    }

    private void Update()
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
            var lookPos = target.transform.position - transform.position;
            lookPos.y = 0;
            var rotation = Quaternion.LookRotation(lookPos);

            agent.SetDestination(target.transform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.CompareTag("Player"))
        {
            var player = collision.collider.gameObject.GetComponent<Player>();
            if (player != null && damageReceived == false)
            {
                Debug.Log(damage);
                ui.ShowDamage();
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
}
