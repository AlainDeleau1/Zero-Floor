using UnityEngine;
using TMPro;
using System.Collections;

public class Player : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int startingHealth = 100;
    public bool died = false;
    private bool damaged = false;
    private PlayerMovement playerMovement;
    private PlayerCam playerCam;
    private PlayerUI playerUI;

    public GameObject cameraHolder;
    private SoundManager sm;
    private GameController gc;
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
        currentHealth = startingHealth;
        maxHealth = startingHealth;

        cameraHolder.gameObject.SetActive(true);
        playerCam = GetComponent<PlayerCam>();
        playerMovement = GetComponent<PlayerMovement>();

        playerUI = FindObjectOfType<PlayerUI>();
        gc = FindObjectOfType<GameController>();
        sm = FindObjectOfType<SoundManager>();

        playerUI.deathText.gameObject.SetActive(false);

        healthHUD.text = currentHealth.ToString();
    }

    private void Update()
    {
        healthHUD.text = currentHealth.ToString();
    }

    private void Die()
    {
        OnPlayerDeath?.Invoke(); //Invoca el Evento
        //playerMovement.enabled = false;
        //
        //playerCam = FindObjectOfType<PlayerCam>();
        //playerCam.enabled = false;
        //
        //Cursor.lockState = CursorLockMode.Confined;
        //Cursor.visible = true;
        //
        //died = true;
        //
        //StartCoroutine(Delay(3.5f));   
    }

    private IEnumerator Delay(float delay)
    {
        yield return new WaitForSeconds(delay);
        damaged = false;
    }

    public void TakeHealth()
    {
        currentHealth = maxHealth;
        healthHUD.text = currentHealth.ToString();
    }
}



