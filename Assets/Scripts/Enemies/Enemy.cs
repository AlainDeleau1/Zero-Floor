using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Threading.Tasks;

public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject target;
    [SerializeField] protected float attackVelocity;
    [SerializeField] protected float inRange;
    [SerializeField] protected NavMeshAgent agent;
    
    protected bool damageReceived = false;
    protected Quaternion angulo;
    protected int currentHealth;
    protected PlayerUI ui;

    private SoundManager sm;
    private GameController gc;

    protected IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(attackVelocity);
        damageReceived = false;
    }

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            sm = FindObjectOfType<SoundManager>();
            sm.EnemyDeadSound();
            gc = FindObjectOfType<GameController>();
            Die();
        }
    }           

    public void Die()
    {
        Destroy(gameObject);
        gc.kills++;
    }

    public async void CheckSphere()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 999f);
        foreach (Collider collider in colliders)
        {
            if (collider != null && collider.CompareTag("Enemy"))
            {
                var enemy = collider.gameObject.GetComponent<Enemy>();
                if (enemy != null)
                {
                    enemy.inRange = 300f;
                    await Task.Delay(5000);
                    if (enemy != null)
                    {
                        enemy.inRange = 15f;
                    }
                }
            }
        }
    }

}
