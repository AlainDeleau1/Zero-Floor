using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject target;
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
        if (gc.killsCounter >= 40)
        {
            sl.PoolKey();
        }
    }
}
