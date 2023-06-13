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


    public PlayerUI ui;
    public GameController gc;
    public GameObject radioBomb;
    public SoundManager sm;
    public PauseMenu pm;
    public SensorLevel sl;
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
        if (sl.departmentOne == true && played == false)
        {
            audioSource.PlayOneShot(slapSlap);
            played = true;
        }
        if (pm.paused == false && sl.departmentOne == true)
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
                    sm.ExplosionDefeat();
                    //StartCoroutine(cs.Shake(duration, magnitude));
                    explosionPlayed = true;
                    Destroy(radioBomb);
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
        currentRadioHP -= damage;
        if (currentRadioHP <= 0)
        {
            Destroy(gameObject);
            sm.ExplosionDefeat();
            explosionPlayed = true;
            Instantiate(explosionEffect, radioBomb.transform.position, radioBomb.transform.rotation);
            audioSource.Stop();
            countdownText.gameObject.SetActive(false);
            sl.departmentOne = false;
            Destroy(sl.gameObject.GetComponent<BoxCollider>());
            ui.victoryMessage.gameObject.SetActive(true);
        }
    }
}
