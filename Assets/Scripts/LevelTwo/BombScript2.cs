using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BombScript2 : MonoBehaviour
{
    public GameController gameController;
    public PlayerUI playerUI;
    public SoundManager soundManager;
    public PauseMenu pauseMenu;

    public CameraShake cameraShake;
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;

    public GameObject level, radioBomb, killsCounter, spawners2;

    bool played = false;
    bool explosionPlayed = false;

    public AudioSource audioSource;
    public AudioClip slapSlap;
    public ParticleSystem explosionEffect;
    public GameObject doorSensors;

    public bool levelTwoIsActive = true;
    public bool demoFinished = false;



    private void Update()
    {
        if (gameController.killsCounter >= 35 && explosionPlayed == false)
        {
            Instantiate(explosionEffect, radioBomb.transform.position, radioBomb.transform.rotation);
            cameraShake.StartCoroutine(cameraShake.Shake(duration, magnitude));
            soundManager.ExplosionDefeat();
            audioSource.Stop();

            playerUI.victoryMessage.gameObject.SetActive(true);

            doorSensors.gameObject.SetActive(true);
            gameController.killsCounter = 0;

            Destroy(spawners2);
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
        levelTwoIsActive = false;
        demoFinished = true;
        print("parlante 2 desactivado");
    }
}
