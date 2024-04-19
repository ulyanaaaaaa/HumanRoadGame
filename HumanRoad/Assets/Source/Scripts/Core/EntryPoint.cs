using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(AudioSource))]
public class EntryPoint : ObjectActivity
{
    [SerializeField] private List<Hurt> _hurts;
    [SerializeField] private List<Hurt> _looseHurts;
    
    [SerializeField] private Canvas _canvas;
    [SerializeField] private RectTransform _hurtsPosition;
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
        _gameInstaller.PlayerCreated.OnDie += () => EnableObject(_gameInstaller.MenuCreated.gameObject);
        _gameInstaller.PlayerCreated.OnDie += DestroyLevel;
    }

    private void Start()
    {
        DisableGame();
        EnableObject(_gameInstaller.MenuCreated.gameObject);
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
            () => DisableObject( _soundMenuCreated.gameObject);
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
        Destroy(_hurts[0].gameObject);
        _hurts.RemoveAt(0);
        _looseHurts.Add(_looseHurtCreated);
    }

    private void SettingMenuButtons()
    {
        _gameInstaller.MenuCreated.GetComponentInChildren<ShopButton>().OnPlay += 
            () => EnableObject(_gameInstaller.ShopCreated.gameObject);
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += 
            () => DisableObject(_gameInstaller.MenuCreated.gameObject);
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += 
            () => EnableObject(_gameInstaller.PlayerCreated.gameObject);
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += 
            () => EnableObject(_gameInstaller.ScoreCounterCreated.gameObject);
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay +=
            () => EnableObject(_gameInstaller.PauseButtonCreated.gameObject);

        foreach (Hurt hurt in _hurts)
            _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay +=
                () => EnableObject(_hurt.gameObject);
        
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += 
            () => EnableObject(_coinsCounterCreated.gameObject);
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += 
            () => EnableObject(_gameInstaller.TimerCreated.gameObject);
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay +=
            () => CreateHurts();

        _gameInstaller.MenuCreated.GetComponentInChildren<SoundButton>().OnPlay += 
            () => EnableObject(_soundMenuCreated.gameObject);
        _gameInstaller.MenuCreated.GetComponentInChildren<LanguageButton>().OnClick += 
            () => EnableObject(_languageMenuCreated.gameObject);

        _gameInstaller.PauseButtonCreated.OnPause += () => EnableObject(_gameInstaller.PauseMenuCreated.gameObject);
        _gameInstaller.PauseMenuCreated.OnPlay += () => DisableObject(_gameInstaller.PauseMenuCreated.gameObject);
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
            () => DisableObject(_languageMenuCreated.gameObject);
    }

    private void CloseShop()
    {
        _gameInstaller.ShopCreated.GetComponentInChildren<ExitButton>().OnExit += () => DisableObject(_gameInstaller.ShopCreated.gameObject);
    }

    private void DestroyLevel()
    {
        foreach (Hurt hurt in _hurts)
            Destroy(hurt.gameObject);
        _hurts.Clear();

        foreach (Hurt looseHurt in _looseHurts) 
            Destroy(looseHurt.gameObject);
        _looseHurts.Clear();
        
        DisableObject(_gameInstaller.PlayerCreated.gameObject);
        DisableObject(_gameInstaller.ScoreCounterCreated.gameObject);
        DisableObject(_coinsCounterCreated.gameObject);
        DisableObject(_gameInstaller.TimerCreated.gameObject);
        DisableObject(_gameInstaller.PauseButtonCreated.gameObject);
    }

    public void CreateGame()
    {
        CloseShop();
        CreateCoinsCounter();
        CreateLanguageMenu();
        CreateSoundMenu();
        SettingMenuButtons();
    }

    private void DisableGame()
    {
        DisableObject(_gameInstaller.PlayerCreated.gameObject);
        DisableObject(_gameInstaller.ScoreCounterCreated.gameObject);
        DisableObject(_gameInstaller.ShopCreated.gameObject);
        DisableObject(_soundMenuCreated.gameObject);
        DisableObject(_coinsCounterCreated.gameObject);
        DisableObject(_languageMenuCreated.gameObject);
        DisableObject(_gameInstaller.MenuCreated.gameObject);
        DisableObject(_gameInstaller.TimerCreated.gameObject);
        DisableObject(_gameInstaller.PauseButtonCreated.gameObject);
        DisableObject(_gameInstaller.PauseMenuCreated.gameObject);
    }
}
