using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxingGloves : MonoBehaviour
{
    public Animator ani;
    public Camera playerCam;
    public int damage = 50;
    RaycastHit rayHit;
    public int range;
    public LayerMask enemies;
    bool damageReceived = false;
    public float pushForce = 500f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            ani.SetBool("Punch", true);

            if (Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out rayHit, range, enemies))
            {
                if (rayHit.collider.gameObject.CompareTag("Enemy"))
                {
                    var enemy = rayHit.collider.gameObject.GetComponent<Enemy>();

                    if (enemy != null && damageReceived == false)
                    {
                        enemy.TakeDamage(damage);

                        Rigidbody enemyRigidbody = rayHit.collider.gameObject.GetComponent<Rigidbody>();
                        if (enemyRigidbody != null)
                        {
                            Vector3 pushDirection = rayHit.collider.gameObject.transform.position - transform.position;
                            pushDirection.Normalize();
                            enemyRigidbody.AddForce(pushDirection * pushForce);
                        }

                        damageReceived = true;
                        StartCoroutine(AttackDelay());

                    }
                }
            }
        }

        else
            ani.SetBool("Punch", false);    
    }

    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(.5f);
        damageReceived = false;
    }
}
