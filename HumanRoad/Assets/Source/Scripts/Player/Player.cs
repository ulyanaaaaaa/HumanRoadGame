using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Zenject;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerWallet))]
public class Player : MonoBehaviour
{
    [SerializeField] private List<GameObject> _skins = new List<GameObject>();
    
    public Action OnLooseHurt;
    public Action OnDie;

    public string Id { get; set; } = "Player";
    
    [SerializeField] private int _maxHealth;
    private Volume _volume;
    private Vignette _vignette;
    private Timer _timer;
    private SaveService _saveService;
    private GameInstaller _gameInstaller; 
    private int _health;
    private string _currentSkin;

    [Inject]
    public void Constructor(Timer timer, GameInstaller gameInstaller)
    {
        _timer = timer;
        _gameInstaller = gameInstaller;
    }

    private void Awake()
    {
        _volume = GetComponentInChildren<Camera>().GetComponent<Volume>();
        
        if (_volume.profile.TryGet(out Vignette vignette))
            _vignette = vignette;
    }
    
    private void OnEnable()
    {
        OnDie += Restart;
    }

    private void Start()
    {
        _saveService = new SaveService();
        
        if (!_saveService.Exists(Id))
        {
            _currentSkin = "Remy";
            Save();
        }
        else
        {
            Load();
        }
        
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += () =>
            transform.position = _gameInstaller.PlayerStartPosition.position;

        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += () =>
            ChangeSkin(_currentSkin);
    }
    
    public void ChangeSkin(string newSkinId)
    {
        foreach (GameObject skin in _skins)
        {
            if (skin.name.Trim() == newSkinId.Trim())
            {
                skin.gameObject.SetActive(true);
                _currentSkin = newSkinId;
                Save();
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
        Camera.main.GetComponent<Animator>().SetTrigger("IsShake"); //
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

    private void Restart()
    {
        _health = _maxHealth;
    }
    
    private void Save()
    {
        PlayerSaveData data = new PlayerSaveData();
        data.CurrentSkin = _currentSkin;
        _saveService.Save(Id, data);
    }

    private void Load()
    {
        _saveService.Load<PlayerSaveData>(Id, data =>
        {
            _currentSkin = data.CurrentSkin;
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
    
    private void OnDisable()
    {
        OnDie -= Restart;
    }
}

public class PlayerSaveData
{
    public string CurrentSkin;
}

