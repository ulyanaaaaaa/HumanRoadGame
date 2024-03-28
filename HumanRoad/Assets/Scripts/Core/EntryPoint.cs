using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SaveService))]
public class EntryPoint : MonoBehaviour
{
    [SerializeField] private List<Hurt> _hurts;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private Vector3 _playerStartPosition;

    private Wallet _wallet;
    private Wallet _walletCreated;
    private TerrainSpawner _terrainSpawner;
    private Menu _menu;
    private Menu _menuCreated;
    private Hurt _hurt;
    private Hurt _hurtCreated;
    private LooseHurt _looseHurt;
    private LooseHurt _looseHurtCreated;
    private Player _player;
    private Player _playerCreated;
    private ScoreCounter _scoreCounter;
    private ScoreCounter _scoreCounterCreated;
    private CoinsCounter _coinsCounter;
    private CoinsCounter _coinsCounterCreated;
    private ShopButton _shopButton;
    private Shop _shop;
    private Shop _shopCreated;
    private Timer _timer;
    private Timer _timerCreated;
    private SaveService _saveService;
    private Pause _pause;
    private Pause _pauseCreated;
    private PauseMenu _pauseMenu;
    private PauseMenu _pauseMenuCreated;
    private SoundMenu _soundMenu;
    private SoundMenu _soundMenuCreated;
    

    private void Awake()
    {
        _saveService = GetComponent<SaveService>();
        _soundMenu = Resources.Load<SoundMenu>(ObjectsPath.SoundMenu);
        _pause = Resources.Load<Pause>(ObjectsPath.Pause);
        _pauseMenu = Resources.Load<PauseMenu>(ObjectsPath.PauseMenu);
        _timer = Resources.Load<Timer>(ObjectsPath.Timer);
        _coinsCounter = Resources.Load<CoinsCounter>(ObjectsPath.CoinsCounter);
        _menu = Resources.Load<Menu>(ObjectsPath.Menu);
        _scoreCounter = Resources.Load<ScoreCounter>(ObjectsPath.ScoreCounter);
        _player = Resources.Load<Player>(ObjectsPath.Player);
        _shop = Resources.Load<Shop>(ObjectsPath.Shop);
        _terrainSpawner = GetComponent<TerrainSpawner>();
        CreateMenu();
    }

    private void CreatePlayer()
    {
        _playerCreated = Instantiate(_player, _playerStartPosition, Quaternion.Euler(0,90,0));
        _terrainSpawner.Setup(_playerCreated.GetComponent<KeyboardInput>());
        _playerCreated.OnLooseHurt += CreateLooseHurt;
        _playerCreated.OnDie += CreateMenu;
        _playerCreated.OnDie += DestroyLevel;
    }

    private void CreateWallet()
    {
        _walletCreated = Instantiate(_wallet,
            _wallet.GetComponent<RectTransform>().localPosition,
            Quaternion.identity,
            _canvas.transform);
        _walletCreated.GetComponent<RectTransform>().localPosition =
            _wallet.GetComponent<RectTransform>().localPosition;
    }

    private void CreateSoundMenu()
    {
        _soundMenuCreated = Instantiate(_soundMenu,
            _soundMenu.GetComponent<RectTransform>().localPosition,
            Quaternion.identity,
            _canvas.transform);
        _soundMenuCreated.GetComponent<RectTransform>().localPosition =
            _soundMenu.GetComponent<RectTransform>().localPosition;
        _soundMenuCreated.GetComponentInChildren<ExitButton>().OnExit += CloseSoundMenu;
    }

    private void CreateCoinsCounter()
    {
        _coinsCounterCreated = Instantiate(_coinsCounter,
            _coinsCounter.GetComponent<RectTransform>().localPosition,
            Quaternion.identity,
            _canvas.transform);
        _coinsCounterCreated.Setup(_playerCreated);
        _coinsCounterCreated.GetComponent<RectTransform>().localPosition =
            _coinsCounter.GetComponent<RectTransform>().localPosition;
    }

    private void CreateScoreCounter()
    {
        _scoreCounterCreated = Instantiate(_scoreCounter,
            _scoreCounter.GetComponent<RectTransform>().localPosition,
            Quaternion.identity,
            _canvas.transform);
        _scoreCounterCreated.GetComponent<RectTransform>().localPosition =
            _scoreCounter.GetComponent<RectTransform>().localPosition;
        _scoreCounterCreated.Setup(_playerCreated.GetComponent<KeyboardInput>());
    }

    private void CreateTimer()
    {
        _timerCreated = Instantiate(_timer,
            new Vector3(2500, 700, 0), //???
            Quaternion.identity,
            _canvas.transform);
        
        _timerCreated.Setup(_playerCreated); 
        _playerCreated.Setup(_timerCreated, _saveService);
    }

    private void CreateHurts()
    {
        _hurt = Resources.Load<Hurt>(ObjectsPath.Hurt);
        
        for (int i = 0; i < 3; i++)
        {
            _hurtCreated = Instantiate(_hurt,
                _hurt.GetComponent<RectTransform>().localPosition + new Vector3(135f * i, 0, 0),
                Quaternion.identity,
                _canvas.transform);
            _hurtCreated.GetComponent<RectTransform>().localPosition =
                _hurt.GetComponent<RectTransform>().localPosition + new Vector3(135f * i, 0, 0);
            _hurts.Add(_hurtCreated);
        }
    }

    private void CreateLooseHurt()
    {
        _looseHurt = Resources.Load<LooseHurt>(ObjectsPath.LooseHurt);
        _looseHurtCreated = Instantiate(_looseHurt,
            _hurts[0].GetComponent<RectTransform>().localPosition,
            Quaternion.identity,
            _canvas.transform);
        _looseHurtCreated.GetComponent<RectTransform>().localPosition =
            _hurts[0].GetComponent<RectTransform>().localPosition;
        _hurts.RemoveAt(0);
    }

    private void CreateMenu()
    {
        _menuCreated = Instantiate(_menu,
            _menu.GetComponent<RectTransform>().localPosition,
            Quaternion.identity,
            _canvas.transform);
        _menuCreated.GetComponent<RectTransform>().localPosition =
            _menu.GetComponent<RectTransform>().localPosition;

        _menuCreated.GetComponentInChildren<ShopButton>().OnPlay += CreateShop;
        _menuCreated.GetComponentInChildren<PlayButton>().OnPlay += DestroyMenu;
        _menuCreated.GetComponentInChildren<PlayButton>().OnPlay += CreatePlayer;
        _menuCreated.GetComponentInChildren<PlayButton>().OnPlay += CreateScoreCounter;
        _menuCreated.GetComponentInChildren<PlayButton>().OnPlay += CreateHurts;
        _menuCreated.GetComponentInChildren<PlayButton>().OnPlay += CreatePause;
        _menuCreated.GetComponentInChildren<PlayButton>().OnPlay += CreateCoinsCounter;
        _menuCreated.GetComponentInChildren<PlayButton>().OnPlay += CreateTimer;
        _menuCreated.GetComponentInChildren<SoundButton>().OnPlay += CreateSoundMenu;
        _menuCreated.GetComponentInChildren<Wallet>().Setup(_saveService);
    }

    private void CreatePause()
    {
        _pauseCreated = Instantiate(_pause,
            _pause.GetComponent<RectTransform>().localPosition,
            Quaternion.identity,
            _canvas.transform);
        _pauseCreated.GetComponent<RectTransform>().localPosition =
            _pause.GetComponent<RectTransform>().localPosition;
        _pauseCreated.OnPause += CreatePauseMenu;
    }

    private void CreatePauseMenu()
    {
        _pauseMenuCreated = Instantiate(_pauseMenu,
            _pauseMenu.GetComponent<RectTransform>().localPosition,
            Quaternion.identity,
            _canvas.transform);
        _pauseMenuCreated.GetComponent<RectTransform>().localPosition =
            _pauseMenu.GetComponent<RectTransform>().localPosition;
        _pauseMenuCreated.OnPlay += ClosePauseMenu;
    }

    private void ClosePauseMenu()
    {
        Destroy(_pauseMenuCreated.gameObject);
    }

    private void CreateShop()
    {
        _shopCreated = Instantiate(_shop,
            _shop.GetComponent<Transform>().localPosition,
            Quaternion.identity,
            _canvas.transform);
        _shopCreated.Setup(_menuCreated.GetComponentInChildren<Wallet>(), _saveService);
        _shopCreated.GetComponent<Transform>().localPosition =
            _shop.GetComponent<Transform>().localPosition;

        _shopCreated.GetComponentInChildren<ExitButton>().OnExit += CloseShop;
    }

    private void CloseShop()
    {
        Destroy(_shopCreated.gameObject);
    }
    
    private void CloseSoundMenu()
    {
        Destroy(_soundMenuCreated.gameObject);
    }

    private void DestroyMenu()
    {
        Destroy(_menuCreated.gameObject);
    }

    private void DestroyLevel()
    {
        if (_hurtCreated)
        {
            foreach (Hurt hurt in _hurts)
                Destroy(hurt.gameObject);
            
            _hurts.Clear();
        }

        if (_looseHurtCreated)
            Destroy(_looseHurtCreated.gameObject);
        
        Destroy(_playerCreated.gameObject);
        Destroy(_scoreCounterCreated.gameObject);
        Destroy(_coinsCounterCreated.gameObject);
        Destroy(_timerCreated.gameObject);
        Destroy(_pauseCreated.gameObject);
    }
}
