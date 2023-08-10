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
    public Collider panCollider;

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

    public virtual void PerformAttack()
    {
        readyToAttack = false;
        anim.SetTrigger("AttackAnim");
        Invoke("ResetAttack", attackRate);
        panCollider.enabled = false;
        anim.SetTrigger("AttackAnim");
    }

    protected void ResetAttack()
    {
        readyToAttack = true;
    }

    protected void Update()
    {
        MyInput();

        if (attacking == false)
            panCollider.enabled = false;
        
        else
            panCollider.enabled = true;
    }
}

