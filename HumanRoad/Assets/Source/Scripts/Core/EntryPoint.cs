using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(SaveService))]
[RequireComponent(typeof(AudioSource))]
public class EntryPoint : WindowsBrain
{
    [SerializeField] private List<Hurt> _hurts;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _hurtsPosition;
    [SerializeField] private RectTransform _pausePosition;
    [SerializeField] private RectTransform _coinsCounterPosition;
    [SerializeField] private RectTransform _scoreCounterPosition;
    [SerializeField] private RectTransform _menuPosition;
    
    [SerializeField] private AudioSource _soundSource;
    [SerializeField] private GameInstaller _gameInstaller;

    [SerializeField] private float _distanceBetweenHurts = 135f;
  
    private Hurt _hurt;
    private Hurt _hurtCreated;
    private Hurt _looseHurt;
    private Hurt _looseHurtCreated;
    private CoinsCounter _coinsCounter;
    private CoinsCounter _coinsCounterCreated;
    private ShopButton _shopButton;
    private Pause _pause;
    private Pause _pauseCreated;
    private PauseMenu _pauseMenu;
    private PauseMenu _pauseMenuCreated;
    private SoundMenu _soundMenu;
    private SoundMenu _soundMenuCreated;
    private LanguageMenu _languageMenu;
    private LanguageMenu _languageMenuCreated;
    private DiContainer _container;

    [Inject]
    public void Container(DiContainer container)
    {
        _container = container;
    }

    private void OnEnable()
    {
        _gameInstaller.PlayerCreated.OnLooseHurt += CreateLooseHurt;
        _gameInstaller.PlayerCreated.OnDie += () => OpenWindow(_gameInstaller.MenuCreated.gameObject);
        _gameInstaller.PlayerCreated.OnDie += DestroyLevel;
    }

    private void Start()
    {
        DisableGame();
        OpenWindow(_gameInstaller.MenuCreated.gameObject);
    }
    
    private void CreateSoundMenu()
    {
        _soundMenu = Resources.Load<SoundMenu>(AssetsPath.SoundMenu);
        _soundMenuCreated = _container.InstantiatePrefabForComponent<SoundMenu>(_soundMenu,
            _soundMenu.GetComponent<RectTransform>().localPosition,
            Quaternion.identity,
            null);
        _soundMenuCreated.transform.SetParent(_canvas.transform, false);
        _soundMenuCreated.GetComponent<RectTransform>().localPosition =
            _soundMenu.GetComponent<RectTransform>().localPosition;
        _soundMenuCreated.GetComponentInChildren<ExitButton>().OnExit += 
            () => CloseWindow( _soundMenuCreated.gameObject);
    }

    private void CreateCoinsCounter()
    {
        _coinsCounter = Resources.Load<CoinsCounter>(AssetsPath.CoinsCounter);
        _coinsCounterCreated = _container.InstantiatePrefabForComponent<CoinsCounter>(_coinsCounter,
            _coinsCounterPosition.GetComponent<RectTransform>().localPosition,
            Quaternion.identity,
            null);
        _coinsCounterCreated.transform.SetParent(_canvas.transform, false);
        _coinsCounterCreated.GetComponent<RectTransform>().localPosition =
            _coinsCounterPosition.GetComponent<RectTransform>().localPosition;
    }

    private void CreateHurts()
    {
        for (int i = 0; i < 3; i++)
        {
            _hurt = Resources.Load<Hurt>(AssetsPath.Hurt);
            _hurtCreated = Instantiate(_hurt,
                _hurtsPosition.GetComponent<RectTransform>().position - new Vector3(_distanceBetweenHurts * i, 0, 0),
                Quaternion.identity,
                _canvas.transform);
            _hurts.Add(_hurtCreated);
        }
    }

    private void CreateLooseHurt()
    {
        _looseHurt = Resources.Load<Hurt>(AssetsPath.LooseHurt);
        _looseHurtCreated = Instantiate(_looseHurt,
        _hurts[0].GetComponent<RectTransform>().position,
            Quaternion.identity,
            _canvas.transform);
        _hurts.RemoveAt(0);
    }

    private void SettingMenuButtons()
    {
        _gameInstaller.MenuCreated.GetComponentInChildren<ShopButton>().OnPlay += 
            () => OpenWindow(_gameInstaller.ShopCreated.gameObject);
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += 
            () => CloseWindow(_gameInstaller.MenuCreated.gameObject);
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += 
            () => OpenWindow(_gameInstaller.PlayerCreated.gameObject);
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += 
            () => OpenWindow(_gameInstaller.ScoreCounterCreated.gameObject);

        foreach (Hurt hurt in _hurts)
            _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay +=
                () => OpenWindow(_hurt.gameObject);
        
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += 
            () => OpenWindow(_pauseCreated.gameObject);
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += 
            () => OpenWindow(_coinsCounterCreated.gameObject);
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += 
            () => OpenWindow(_gameInstaller.TimerCreated.gameObject);
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay +=
            () => CreateHurts();

        _gameInstaller.MenuCreated.GetComponentInChildren<SoundButton>().OnPlay += 
            () => OpenWindow(_soundMenuCreated.gameObject);
        _gameInstaller.MenuCreated.GetComponentInChildren<LanguageButton>().OnClick += 
            () => OpenWindow(_languageMenuCreated.gameObject);
    }

    private void CreatePause()
    {
        _pause = Resources.Load<Pause>(AssetsPath.Pause);
        _pauseCreated = Instantiate(_pause,
            _pausePosition.GetComponent<RectTransform>().position,
            Quaternion.identity,
            _canvas.transform);
        _pauseCreated.OnPause += () => OpenWindow(_pauseMenuCreated.gameObject);
    }

    private void CreatePauseMenu()
    { 
        _pauseMenu = Resources.Load<PauseMenu>(AssetsPath.PauseMenu);
        _pauseMenuCreated = _container.InstantiatePrefabForComponent<PauseMenu>(_pauseMenu,
            _pauseMenu.GetComponent<RectTransform>().localPosition,
            Quaternion.identity,
            null);
        _pauseMenuCreated.transform.SetParent(_canvas.transform, false);
        _pauseMenuCreated.GetComponent<RectTransform>().localPosition =
            _pauseMenu.GetComponent<RectTransform>().localPosition;
        _pauseMenuCreated.OnPlay += () => CloseWindow(_pauseMenuCreated.gameObject);
    }

    private void CreateLanguageMenu()
    {
        _languageMenu = Resources.Load<LanguageMenu>(AssetsPath.LanguageMenu);
        _languageMenuCreated = _container.InstantiatePrefabForComponent<LanguageMenu>(_languageMenu,
            _languageMenu.GetComponent<RectTransform>().localPosition,
            Quaternion.identity,
            null);
        _languageMenuCreated.transform.SetParent(_canvas.transform, false);
        _languageMenuCreated.GetComponent<RectTransform>().localPosition =
            _languageMenu.GetComponent<RectTransform>().localPosition;
        _languageMenuCreated.GetComponentInChildren<ExitButton>().OnExit +=
            () => CloseWindow(_languageMenuCreated.gameObject);
    }

    private void CloseShop()
    {
        _gameInstaller.ShopCreated.GetComponentInChildren<ExitButton>().OnExit += () => CloseWindow(_gameInstaller.ShopCreated.gameObject);
    }

    private void DestroyLevel()
    {
        if (_hurtCreated)
        {
            foreach (Hurt hurt in _hurts)
                CloseWindow(hurt.gameObject);
            
            _hurts.Clear();
        }

        if (_looseHurtCreated)
            CloseWindow(_looseHurtCreated.gameObject);
        
        CloseWindow(_gameInstaller.PlayerCreated.gameObject);
        CloseWindow(_gameInstaller.ScoreCounterCreated.gameObject);
        CloseWindow(_coinsCounterCreated.gameObject);
        CloseWindow(_gameInstaller.TimerCreated.gameObject);
        CloseWindow(_pauseCreated.gameObject);
    }

    public void CreateGame()
    {
        CreateHurts();
        SettingMenuButtons();
        CreatePause();
        CloseShop();
        CreateCoinsCounter();
        CreateLanguageMenu();
        CreateLooseHurt();
        CreatePauseMenu();
        CreateSoundMenu();
    }

    private void DisableGame()
    {
        CloseWindow(_gameInstaller.PlayerCreated.gameObject);

        for (int i = 0; i < _hurts.Count; i++)
        {
            
            CloseWindow(_hurts[i].gameObject);
        }
        CloseWindow(_pauseCreated.gameObject);
        CloseWindow(_gameInstaller.ScoreCounterCreated.gameObject);
        CloseWindow(_gameInstaller.ShopCreated.gameObject);
        CloseWindow(_soundMenuCreated.gameObject);
        CloseWindow(_coinsCounterCreated.gameObject);
        CloseWindow(_languageMenuCreated.gameObject);
        CloseWindow(_gameInstaller.MenuCreated.gameObject);
        CloseWindow(_pauseMenuCreated.gameObject);
        CloseWindow(_gameInstaller.TimerCreated.gameObject);
    }
}
