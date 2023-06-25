using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject target, pill;
    [SerializeField] protected float attackVelocity;
    [SerializeField] protected float inRange;
    [SerializeField] protected NavMeshAgent agent;
    [SerializeField] protected SensorLevel sl;
    [SerializeField] protected PlayerUI ui;
    [SerializeField] protected SoundManager sm;
    [SerializeField] protected GameController gc;
    
    protected bool damageReceived = false;
    protected Quaternion angulo;
    protected int currentHealth;

    private bool died = false;

    protected IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackVelocity);
        damageReceived = false;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0 && died == false)
        {
            sm = FindObjectOfType<SoundManager>();
            sm.EnemyDeadSound();
            gc = FindObjectOfType<GameController>();
            Die();
            died = true;
        }
    }           

    public void Die()
    {
        Destroy(gameObject);
        gc.killsCounter++;
        gc.kills++;
        int healChance = Random.Range(0, 100);
        //int bluePillChance = Random.Range(0, 100);
        if (healChance <= 7)
        {
            Vector3 pillSpawnPosition = agent.transform.position + new Vector3(0f, 1f, 0f);
            GameObject newPill = Instantiate(pill, pillSpawnPosition, Quaternion.identity);
            Destroy(newPill, 10f);
        }

        //else if (bluePillChance <= 7 && instantiated == false)
        //{
        //    Vector3 pillSpawnPosition = agent.transform.position + new Vector3(0f, 1f, 0f);
        //    GameObject newPill = Instantiate(bluePill, pillSpawnPosition, Quaternion.identity);
        //    Destroy(newPill, 5f);
        //    instantiated = true;
        //}

        if (gc.killsCounter >= 30)
        {
            sl.PoolKey();
        }
    }
}
