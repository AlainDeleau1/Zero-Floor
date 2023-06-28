using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BombScript : MonoBehaviour
{
    public GameController gameController;
    public PlayerUI playerUI;
    public SoundManager soundManager;
    public PauseMenu pauseMenu;

    public CameraShake cameraShake;
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;

    public GameObject level;
    public GameObject enemies;
    public GameObject radioBomb;

    bool played = false;
    bool explosionPlayed = false;

    public AudioSource audioSource;
    public AudioClip slapSlap;
    public ParticleSystem explosionEffect;
    public GameObject doorSensors;

    private void Update()
    {
        if (gameController.killsCounter >= 35 && explosionPlayed == false)
        {
            enemies.gameObject.SetActive(false);
            Destroy(gameObject);
            level.gameObject.SetActive(false);

            Instantiate(explosionEffect, radioBomb.transform.position, radioBomb.transform.rotation);
            cameraShake.StartCoroutine(cameraShake.Shake(duration, magnitude));
            soundManager.ExplosionDefeat();
            audioSource.Stop();

            playerUI.victoryMessage.gameObject.SetActive(true);

            doorSensors.gameObject.SetActive(true);
            gameController.killsCounter = 0;

            explosionPlayed = true;
        }

        if (played == false)
        {
            audioSource.PlayOneShot(slapSlap);
            played = true;
        }
    }     
    
}
