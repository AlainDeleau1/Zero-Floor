using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public Slider slider;

    private float volume;
    public AudioSource[] musicAudioSources;

    private void Start()
    {
        volume = 0.1f;
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
        foreach (AudioSource source in musicAudioSources)
        {
            source.volume = volume;
        }
    }
}


