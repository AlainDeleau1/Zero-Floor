using UnityEngine;
using TMPro;
using System.Collections;

public class Player : MonoBehaviour
{


    public int currentHealth;
    private int startingHealth = 100;
    public bool died = false;
    private bool damaged = false;
    private PlayerMovement playerMovement;
    private PlayerCam playerCam;
    private PlayerUI playerUI;

    public GameObject cameraHolder;
    [SerializeField] private SoundManager sm;
    [SerializeField] private LayerMask Weapon;
    [SerializeField] private new Camera camera;
    [SerializeField] private GameController gc;
    [SerializeField] public TextMeshProUGUI healthHUD;

    public delegate void PlayerDeathEventHandler();
    public static event PlayerDeathEventHandler OnPlayerDeath;

    public void TakeDamage(int damage)
    {
        if (damaged == false)
        {
            currentHealth -= damage;
            sm.EnemyAttackSound();
            sm.PlayerDamagedSound();
            healthHUD.text = currentHealth.ToString();
            playerUI.ShowDamage(2);
            StartCoroutine(Delay(.75f));
            damaged = true;
        }

        if (currentHealth <= 0 && died == false)
        {
            sm.PlayerDamagedSound();
            currentHealth = 0;
            Die();
            died = true;
        }

        if (died == true)
        {
            healthHUD.text = "0";
        }
    }

    private void Start()
    {
        cameraHolder.gameObject.SetActive(true);
        playerCam = GetComponent<PlayerCam>();
        playerMovement = GetComponent<PlayerMovement>();

        playerUI = FindObjectOfType<PlayerUI>();
        playerUI.deathText.gameObject.SetActive(false);

        currentHealth = startingHealth;

        healthHUD.text = currentHealth.ToString();
    }

    private void Die()
    {
        playerUI.deathText.gameObject.SetActive(true);
        
        playerMovement.enabled = false;

        playerCam = FindObjectOfType<PlayerCam>();
        playerCam.enabled = false;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        died = true;

        StartCoroutine(Delay(3.5f));
        

        OnPlayerDeath?.Invoke(); //Invoca el Evento
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        damaged = false;
    }

}



