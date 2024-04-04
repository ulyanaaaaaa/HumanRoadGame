using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(TextTranslator))]
[RequireComponent(typeof(AudioSource))]
public class MusicSlider : MonoBehaviour
{
    private Slider _slider;
    private AudioSource _audioSource;  
    
    private void Awake()
    {
        GetComponent<TextTranslator>().Id = "music";
        _audioSource = GetComponent<AudioSource>();
        _slider = GetComponentInChildren<Slider>();
        
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            _audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
            _slider.value = _audioSource.volume;
        }
    }
}
