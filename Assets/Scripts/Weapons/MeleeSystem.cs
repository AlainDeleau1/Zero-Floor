using UnityEngine;

public abstract class MeleeSystem : MonoBehaviour
{
    [Header("Stats")]
    public int damage;
    public float range;
    public float attackRate;
    public bool pickedUp;
    public bool readyToAttack;
    public bool attacking;

    [Header("References")]
    public Player p;
    public SoundManager sm;
    public PlayerUI ui;
    public Animator anim;
    public LayerMask whatIsEnemy;

    [Header("Particles")]
    public GameObject hitEffect;

    protected void Awake()
    {
        readyToAttack = true;
    }

    public virtual void MyInput()
    {
        if (p.died == false)
        {
            attacking = Input.GetKeyDown(KeyCode.Mouse0);

        }
    }

    protected void PerformAttack()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, range, whatIsEnemy))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                enemy.TakeDamage(damage);
            }
        }

        Instantiate(hitEffect, transform.position, Quaternion.LookRotation(hit.normal));

        readyToAttack = false;
        Invoke("ResetAttack", attackRate);
        anim.SetTrigger("AttackAnim");
    }

    protected void ResetAttack()
    {
        readyToAttack = true;
    }

    protected void Update()
    {
        MyInput();
    }
}

