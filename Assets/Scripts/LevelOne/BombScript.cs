using UnityEngine;

public class BombScript : MonoBehaviour
{
    public GameController gameController;
    public PlayerUI playerUI;
    public SoundManager soundManager;
    public PauseMenu pauseMenu;

    public Transform spawnRifle;

    public CameraShake cameraShake;
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;

    public GameObject level, radioBomb, spawners;

    bool played = false;
    bool explosionPlayed = false;

    public AudioSource audioSource;
    public AudioClip slapSlap;
    public ParticleSystem explosionEffect;
    public GameObject rifle, killsCounter;

    private void Update()
    {
        if (gameController.killsCounter >= 35 && explosionPlayed == false)
        {
            Instantiate(explosionEffect, radioBomb.transform.position, radioBomb.transform.rotation);
            Instantiate(rifle, spawnRifle.transform.position, spawnRifle.transform.rotation);
            cameraShake.StartCoroutine(cameraShake.Shake(duration, magnitude));
            soundManager.ExplosionDefeat();
            audioSource.Stop();

            playerUI.victoryMessage.gameObject.SetActive(true);
            gameController.killsCounter = 0;

            Destroy(spawners);
            radioBomb.gameObject.SetActive(false);
            level.gameObject.SetActive(false);

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
