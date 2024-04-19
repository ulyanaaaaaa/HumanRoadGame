using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(TextTranslator))]
public class MusicSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    private AudioSource _audioSource;  
    
    [Inject]
    public void Container(AudioSource audioSource)
    {
        _audioSource = audioSource;
    }
    
    private void Awake()
    {
        GetComponent<TextTranslator>().Id = "music";
        _slider = GetComponentInChildren<Slider>();
        _slider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            _slider.value = PlayerPrefs.GetFloat("MusicVolume");
            ChangeMusicVolume(); 
        }
    }

    public void ChangeMusicVolume()
    {
        if (_audioSource == null)
            return;

        PlayerPrefs.SetFloat("MusicVolume", _slider.value);
        _audioSource.volume = PlayerPrefs.GetFloat("MusicVolume");
    }
}