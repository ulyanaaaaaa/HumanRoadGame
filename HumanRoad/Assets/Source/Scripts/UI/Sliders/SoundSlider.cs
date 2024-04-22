using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(TextTranslator))]
public class SoundSlider : MonoBehaviour
{
    private Slider _slider;
    private ISaveService _saveService;
    private AudioSource _audioSource;
    private string _id = "sound";
    
    [Inject]
    public void Constructor(AudioSource audioSource)
    {
        _audioSource = audioSource;
    }
    
    private void Awake()
    {
        GetComponent<TextTranslator>().Id = _id;
        _slider = GetComponentInChildren<Slider>();
        _slider.onValueChanged.AddListener(delegate { ChangeSoundVolume(); });
    }
    
    private void Start()
    {
        _saveService = new SaveService();
        
        if (!_saveService.Exists(_id))
        {
            _audioSource.volume = 50f;
            Save();
        }
        else
        {
            Load();
        }
    }
    
    private void ChangeSoundVolume()
    {
        _audioSource.volume = _slider.value;
        Save();
    }
    
    private void Save()
    {
        SoundSliderSaveData e = new SoundSliderSaveData();
        e.SoundSliderValue = _slider.value;
        _saveService.Save(_id, e);
    }

    private void Load()
    {
        _saveService.Load<SoundSliderSaveData>(_id, e =>
        {
            _audioSource.volume = e.SoundSliderValue;
        });
        _slider.value = _audioSource.volume;
    }
}

public class SoundSliderSaveData
{
    public float SoundSliderValue { get; set; }
}
