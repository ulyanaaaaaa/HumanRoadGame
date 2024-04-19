using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(TextTranslator))]
public class SoundSlider : MonoBehaviour
{
    private Slider _slider;
    private AudioSource _audioSource;

    [Inject]
    public void Container(AudioSource audioSource)
    {
        _audioSource = audioSource;
    }
    
    private void Awake()
    {
        GetComponent<TextTranslator>().Id = "sound";
        _slider = GetComponentInChildren<Slider>();
        _slider.onValueChanged.AddListener(delegate { ChangeSoundVolume(); });
        
        if (PlayerPrefs.HasKey("SoundVolume"))
        {
            _slider.value = PlayerPrefs.GetFloat("SoundVolume");
            ChangeSoundVolume(); 
        }
    }
    
    public void ChangeSoundVolume()
    {
        if (_audioSource == null)
            return;
        
        PlayerPrefs.SetFloat("SoundVolume", _slider.value);
        _audioSource.volume = PlayerPrefs.GetFloat("SoundVolume");
    }
}
