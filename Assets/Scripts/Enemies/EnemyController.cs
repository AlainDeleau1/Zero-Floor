using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform gunTip; // Transform del objeto desde donde sale el proyectil
    public float fireRate = 0.5f; // Tiempo en segundos entre cada disparo
    public float bulletSpeed = 10f; // Velocidad del proyectil
    public float detectionRange = 10f; // Distancia m�xima de detecci�n
    public LayerMask playerLayer; // Capa del jugador

    private Transform player; // Transform del jugador
    private float lastFireTime; // �ltimo tiempo en que el enemigo dispar�

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Comprobar si el jugador est� dentro del rango de detecci�n
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            // Comprobar si el jugador est� en l�nea de visi�n
            RaycastHit hit;
            if (Physics.Raycast(transform.position, player.position - transform.position, out hit, detectionRange, playerLayer))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    // Disparar si ha pasado suficiente tiempo desde el �ltimo disparo
                    if (Time.time - lastFireTime > fireRate)
                    {
                        Shoot();
                        lastFireTime = Time.time;
                    }
                }
            }
        }
    }

    void Shoot()
    {
        // Crear una instancia del proyectil y lanzarlo hacia el jugador
        GameObject bullet = Instantiate(bulletPrefab, gunTip.position, Quaternion.identity);
        Vector3 direction = (player.position - gunTip.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
    }
}
