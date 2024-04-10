using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerWallet))]
public class Player : MonoBehaviour, ISaveData
{
    public Action OnLooseHurt;
    public Action OnDie;
    
    public string Id { get; set; }
    
    [SerializeField] private List<GameObject> _skins = new List<GameObject>();
    private string _currentSkin;
    
    [SerializeField] private int _health;
    [SerializeField] private int _maxHealth;
    
    private Volume _volume;
    private Vignette _vignette;
    private Timer _timer;
    private SaveService _saveService;
    private GameInstaller _gameInstaller;

    [Inject]
    public void Container(Timer timer, SaveService saveService, GameInstaller gameInstaller)
    {
        _saveService = saveService;
        _timer = timer;
        _gameInstaller = gameInstaller;
    }

    public void Save()
    {
        _saveService.SaveData.AddData(Id, new PlayerSaveData(Id, typeof(Player), _currentSkin));
        Debug.Log("SavePLayer " + _currentSkin);
        _saveService.Save();
    }

    public void Load()
    {
        Debug.Log("Load");
        if (_saveService.SaveData.TryGetData(Id, out PlayerSaveData playerSaveData))
        {
            _currentSkin = playerSaveData.Skin;
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" + _currentSkin);
        }
    }
    
    private void OnEnable()
    {
        OnDie += Save;
        OnDie += Revival;
    }

    private void Awake()
    {
        Id = "Player";
        _volume = GetComponentInChildren<Camera>().GetComponent<Volume>();
        
        if (_volume.profile.TryGet(out Vignette vignette))
            _vignette = vignette;
    }

    private void Start()
    {
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += () =>
            transform.position = _gameInstaller.PlayerStartPosition.position;
        Load();
        ChangeSkin(_currentSkin);
    }
    
    private void ChangeSkin(string newSkin)
    {
        Debug.Log("nEWsKIN: " + newSkin);
        foreach (GameObject skin in _skins)
        {
            if (skin.name == newSkin)
            {
                skin.gameObject.SetActive(true);
                _currentSkin = newSkin;
            }
            else
            {
                skin.gameObject.SetActive(false);
            }
        }
    }

    public void TakeDamage()
    {
        VignetteEffect();
        OnLooseHurt?.Invoke();
        Camera.main.GetComponent<Animator>().SetTrigger("IsShake");
        _health--;

        if (_health == 0)
            OnDie?.Invoke();
    }

    private void VignetteEffect()
    {
        float vignetteValue = 0;
        DOTween.To(() => vignetteValue, x => vignetteValue = x, 0.5f, 0.5f).OnUpdate(() =>
        {
            _vignette.intensity.value = vignetteValue;
            _vignette.smoothness.value = vignetteValue;
        }).OnComplete(() =>
        {
            DOTween.To(() => vignetteValue, x => vignetteValue = x, 0f, 0.5f).OnUpdate(() =>
            {
                _vignette.intensity.value = vignetteValue;
                _vignette.smoothness.value = vignetteValue;
            });
        });
    }
    
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.TryGetComponent(out TimeCoin coin))
        {
            StartCoroutine(_timer.StopTimerCoroutine(coin.Time));
            Destroy(coin.gameObject);
        }
    }

    private void Revival()
    {
        _health = 3;
    }
}

[Serializable]
public class PlayerSaveData : SaveData
{
    public string Skin;
    
    public PlayerSaveData(string id, Type type, string skin) : base(id, type)
    {
        Skin = skin;
    }
}
