using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextTranslator))]
[RequireComponent(typeof(AudioSource))]
public class SoundSlider : MonoBehaviour
{
    private Slider _slider;
    private AudioSource _audioSource;
    
    private void Awake()
    {
        GetComponent<TextTranslator>().SetId("sound");
        _audioSource = GetComponent<AudioSource>();
        _slider = GetComponentInChildren<Slider>();
        
        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            _audioSource.volume = PlayerPrefs.GetFloat("SoundVolume");
            _slider.value = _audioSource.volume;
        }
    }
}
