using UnityEngine;
using System.Threading.Tasks;

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

    private async void Update()
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

            await Task.Delay(3000);
            if (gameController != null)
            {
                gameController.ChangeScene(GameController.SceneNames.FinalScene);
            }
            else
            {
                Debug.LogWarning("El GameController no está en la escena o no tiene el tag adecuado.");
            }

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
