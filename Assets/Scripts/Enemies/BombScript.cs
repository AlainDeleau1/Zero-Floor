using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BombScript : Enemy
{
    public TextMeshProUGUI countdownText;
    [SerializeField] private int countdownValue;
    [SerializeField] private float timer;
    public CameraShake cs;
    [SerializeField] private float duration;
    [SerializeField] private float magnitude;

    public int radioHP;
    public int currentRadioHP;
    public GameObject enemies;
    public GameObject radioBomb;
    public PauseMenu pm;
    public AudioSource audioSource;
    public AudioClip slapSlap;
    public ParticleSystem explosionEffect;
    
    bool played = false;
    bool explosionPlayed = false;

    private void Start()
    {
        currentRadioHP = radioHP;
        countdownText.text = countdownValue.ToString();
    }

    private void Update()
    {
        if (played == false)
        {
            audioSource.PlayOneShot(slapSlap);
            played = true;
        }

        if (pm.paused == false)
        {
            countdownText.gameObject.SetActive(true);
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                countdownValue--;
                if (countdownValue >= 0)
                {
                    countdownText.text = countdownValue.ToString();
                    timer = 1f;
                }
                else if (explosionPlayed == false)
                {
                    enemies.SetActive(false);
                    cs.StartCoroutine(cs.Shake(duration, magnitude));
                    Destroy(radioBomb);
                    sm.ExplosionDefeat();
                    explosionPlayed = true;
                    Instantiate(explosionEffect, radioBomb.transform.position, radioBomb.transform.rotation);
                    audioSource.Stop();
                    ui.deathText.gameObject.SetActive(true);
                    countdownText.gameObject.SetActive(false);
                    gc.RestartLevel();
                }
            }
        }
    }

    public override void TakeDamage(int damage)
    {
        if (sl.boolean == false)
        {
            currentRadioHP -= damage;
            if (currentRadioHP <= 0)
            {
                enemies.SetActive(false);
                cs.StartCoroutine(cs.Shake(duration, magnitude));
                Destroy(gameObject);
                sm.ExplosionDefeat();
                explosionPlayed = true;
                Instantiate(explosionEffect, radioBomb.transform.position, radioBomb.transform.rotation);
                audioSource.Stop();
                countdownText.gameObject.SetActive(false);
                ui.victoryMessage.gameObject.SetActive(true);
            }
        }  
    }
}
