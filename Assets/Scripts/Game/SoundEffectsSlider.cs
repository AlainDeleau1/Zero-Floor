using UnityEngine;
using UnityEngine.UI;

public class SoundEffectsSlider : MonoBehaviour
{
    public Slider slider;

    private float volume;
    public AudioSource[] sfxAudioSources;
    private PlayerData pd;

    private void Start()
    {
        pd = FindObjectOfType<PlayerData>();
        volume = pd.sfxVolume;
        slider.value = volume;
        slider.minValue = 0f;
        slider.maxValue = 1f;
        slider.onValueChanged.AddListener(delegate { OnSliderValueChanged(); });

        SetVolume(volume);
    }

    private void OnSliderValueChanged()
    {
        volume = slider.value;
        SetVolume(volume);
    }

    private void SetVolume(float volume)
    {
        foreach (AudioSource source in sfxAudioSources)
        {
            source.volume = volume;
            pd.sfxVolume = volume;
        }
    }
}



