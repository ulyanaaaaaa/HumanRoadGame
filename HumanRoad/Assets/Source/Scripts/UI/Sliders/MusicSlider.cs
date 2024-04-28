using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(TextTranslator))]
public class MusicSlider : MonoBehaviour
{
    private Slider _slider;
    private ISaveService _saveService;
    private AudioSource _audioSource;
    private string _id = "music";
    
    [Inject]
    public void Constructor(AudioSource audioSource)
    {
        _audioSource = audioSource;
    }
    
    private void Awake()
    {
        GetComponent<TextTranslator>().Id = _id;
        _slider = GetComponentInChildren<Slider>();
    }

    private void Start()
    {
        _slider.onValueChanged.AddListener(delegate { ChangeMusicVolume(); });
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
    
    private void ChangeMusicVolume()
    {
        _audioSource.volume = _slider.value;
        Save();
    }
    
    private void Save()
    {
        MusicSliderSaveData e = new MusicSliderSaveData();
        e.MusicSliderValue = _slider.value;
        _saveService.Save(_id, e);
    }

    private void Load()
    {
        _saveService.Load<MusicSliderSaveData>(_id, e =>
        {
            _audioSource.volume = e.MusicSliderValue;
        });
        _slider.value = _audioSource.volume;
    }
}

public class MusicSliderSaveData
{
    public float MusicSliderValue { get; set; }
}