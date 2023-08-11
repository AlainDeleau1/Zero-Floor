using UnityEngine;

public class BombScript : MonoBehaviour
{
    public Player player;
    public GameController gameController;
    public PlayerUI playerUI;
    public SoundManager soundManager;
    public PauseMenu pauseMenu;

    public CameraShake cameraShake;
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;
    public GameObject level, radioBomb, spawners;
    public EnemySpawners es;
    bool played = false;
    public bool explosionPlayed = false;

    public AudioSource audioSource;
    public AudioClip slapSlap;
    public ParticleSystem explosionEffect;
    public GameObject killsCounter;

    private void Start()
    {
        es = FindObjectOfType<EnemySpawners>();
    }

    private void Update()
    {
        if (gameController.killsCounter >= es.maxKills && explosionPlayed == false)
        {
            Instantiate(explosionEffect, radioBomb.transform.position, radioBomb.transform.rotation);
            cameraShake.StartCoroutine(cameraShake.Shake(duration, magnitude));
            soundManager.ExplosionDefeat();
            audioSource.Stop();

            playerUI.victoryMessage.gameObject.SetActive(true);
            gameController.killsCounter = 0;

            Destroy(spawners);
            radioBomb.gameObject.SetActive(false);
            level.gameObject.SetActive(false);

            player.currentHealth = 100;

            explosionPlayed = true;
        }

        if (played == false)
        {
            audioSource.PlayOneShot(slapSlap);
            played = true;
        }
    }

    private void OnDisable()
    {
        killsCounter.gameObject.SetActive(false);
        DestroyEnemies();
    }

    private void DestroyEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            enemy.Die(4f);
        }
    }
}
