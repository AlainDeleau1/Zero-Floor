using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundMan : MonoBehaviour

{
    public static SoundMan Instance;

    private Dictionary<string, AudioSource> soundDictionary;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
           Destroy(gameObject);

       DontDestroyOnLoad(gameObject);

        soundDictionary = new Dictionary<string, AudioSource>(); // Diccionario establecido
        LoadSounds();
    }

    private void LoadSounds()
    {
        AudioClip onClickedSound = Resources.Load<AudioClip>("OnClicked");
        AudioClip onEntrySound = Resources.Load<AudioClip>("OnEntry");

        AudioSource onClickedAudioSource = gameObject.AddComponent<AudioSource>(); //Configura los AudioSource
        onClickedAudioSource.clip = onClickedSound;
        AudioSource onEntryAudioSource = gameObject.AddComponent<AudioSource>();
        onEntryAudioSource.clip = onEntrySound;

        soundDictionary.Add("OnClicked", onClickedAudioSource);
        soundDictionary.Add("OnEntry", onEntryAudioSource);
    }

    public void PlaySound(string soundName)
    {
        if (soundDictionary.TryGetValue(soundName, out AudioSource audioSource))
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No se encontro el sonido" + soundName);
        }
    }
}
