using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public int currentHealth;

    [SerializeField] private TextMeshProUGUI healthHUD;

    private int startingHealth = 100;
    public bool died = false;
    private PlayerMovement playerMovement;
    private PlayerCam playerCam;
    private PlayerUI playerUI;

    [SerializeField] private SoundManager sm;
    [SerializeField] private LayerMask Weapon;
    [SerializeField] private new Camera camera;
    [SerializeField] private GameController gc;
    
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        sm.EnemyAttackSound();
        healthHUD.text = currentHealth.ToString();
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

        gc.RestartLevel();
    }
}



