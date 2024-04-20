using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private RectTransform _pausePosition;
    [SerializeField] public Transform PlayerStartPosition;
    [SerializeField] private Translator _translator;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Transform _scoreCounterPosition;
    [SerializeField] private RectTransform _timerPosition;
    [SerializeField] private RectTransform _menuPosition;
    [SerializeField] private EntryPoint _entryPoint;
    [SerializeField] private PauseService _pauseService;
    
    public Menu MenuCreated;
    public Player PlayerCreated;
    public ScoreCounter ScoreCounterCreated;
    public Timer TimerCreated;
    public Shop ShopCreated;
    private Shop _shop;
    private Menu _menu;
    private ScoreCounter _scoreCounter;
    private Timer _timer;
    private Player _player;
    private Wallet _wallet;
    private PauseButton _pauseButton;
    public PauseButton PauseButtonCreated;
    private PauseMenu _pauseMenu;
    public PauseMenu PauseMenuCreated;

    public override void InstallBindings()
    {
        Container.Bind<GameInstaller>()
            .FromInstance(this)
            .AsSingle();
        
        Container.Bind<AudioSource>()
            .FromInstance(_audioSource)
            .AsSingle();
        
        Container.Bind<Translator>()
            .FromInstance(_translator)
            .AsSingle();
        
        Container.Bind<PauseService>()
            .FromInstance(_pauseService)
            .AsSingle();
        
        MenuBind();
        
        Container.Bind<Wallet>()
            .FromInstance(MenuCreated.GetComponentInChildren<Wallet>())
            .AsSingle();
        
        PauseBind();
        PauseMenuBind();
        TimerBind();
        PlayerBind();
        
        Container.Bind<KeyboardInput>()
            .FromInstance(PlayerCreated.GetComponent<KeyboardInput>())
            .AsSingle();

        Container.Bind<PlayerMovement>()
            .FromInstance(PlayerCreated.GetComponent<PlayerMovement>())
            .AsSingle(); 
        
        ShopBind();
        ScoreCounterBind();
        CreateGame();
    }

    private void OnEnable()
    {
        PlayerCreated.OnDie += SetPlayerStartPosition;
    }

    private void SetPlayerStartPosition()
    {
        PlayerCreated.transform.position = PlayerStartPosition.position;
    }

    private void PlayerBind()
    {
        _player = Resources.Load<Player>(AssetsPath.Player);
        
        PlayerCreated = Instantiate(_player
            ,PlayerStartPosition.GetComponent<Transform>().position
            ,Quaternion.Euler(0,90,0));
        PlayerCreated.transform.position = PlayerStartPosition.GetComponent<Transform>().position;
        
        Container.Bind<Player>()
            .FromInstance(PlayerCreated)
            .AsSingle();
        
        TimerCreated.Container(Container.Resolve<Player>(), this, _pauseService);
        PlayerCreated.Container(Container.Resolve<Timer>(), this);
        PlayerCreated.GetComponent<PlayerMovement>().Constructor(_pauseService);
        PlayerCreated.GetComponent<PlayerWallet>().Constructor(this);
    }

    private void MenuBind()
    {
        _menu = Resources.Load<Menu>(AssetsPath.Menu);
        MenuCreated = Instantiate(_menu,
            _menuPosition.GetComponent<RectTransform>().position,
            Quaternion.identity,
            null);
        MenuCreated.transform.SetParent(_canvas.transform, false);
        MenuCreated.transform.position = _menuPosition.GetComponent<RectTransform>().position;
        MenuCreated.Container(Container.Resolve<Translator>(), this);
        MenuCreated.GetComponentInChildren<Wallet>().Container(this);
    }

    private void TimerBind()
    {
        _timer = Resources.Load<Timer>(AssetsPath.Timer);
        TimerCreated = Instantiate(_timer,
            _timerPosition.GetComponent<RectTransform>().position, 
            Quaternion.identity,
            null);
        TimerCreated.transform.SetParent(_canvas.transform, false);
        TimerCreated.transform.position = _timerPosition.GetComponent<RectTransform>().position;
        Container.Bind<Timer>()
            .FromInstance(TimerCreated)
            .AsSingle();
    }

    private void ShopBind()
    {
        _shop = Resources.Load<Shop>(AssetsPath.Shop);
        ShopCreated = Instantiate(_shop,
            _shop.GetComponent<Transform>().localPosition,
            Quaternion.identity,
            null);
        ShopCreated.transform.SetParent(_canvas.transform, false);
        ShopCreated.GetComponent<Transform>().localPosition =
            _shop.GetComponent<Transform>().localPosition;
        
        ShopCreated.Container(Container.Resolve<Wallet>(), _translator, PlayerCreated);
    }

    private void ScoreCounterBind()
    {
        _scoreCounter = Resources.Load<ScoreCounter>(AssetsPath.ScoreCounter);
        ScoreCounterCreated = Instantiate(_scoreCounter,
            _scoreCounterPosition.GetComponent<RectTransform>().position,
            Quaternion.identity,
            null);
        ScoreCounterCreated.transform.SetParent(_canvas.transform, false);
        ScoreCounterCreated.GetComponent<RectTransform>().localPosition =
            _scoreCounterPosition.GetComponent<RectTransform>().localPosition;
        ScoreCounterCreated.Constructor(Container.Resolve<PlayerMovement>());
    }
    
    private void PauseBind()
    {
        _pauseButton = Resources.Load<PauseButton>(AssetsPath.Pause);
        PauseButtonCreated = Instantiate(_pauseButton,
            _pausePosition.GetComponent<RectTransform>().position,
            Quaternion.identity,
            _canvas.transform); 
        PauseButtonCreated.Constructor(_pauseService);
    }

    private void PauseMenuBind()
    { 
        _pauseMenu = Resources.Load<PauseMenu>(AssetsPath.PauseMenu);
        PauseMenuCreated = Instantiate(_pauseMenu,
            _pauseMenu.GetComponent<RectTransform>().localPosition,
            Quaternion.identity,
            null);
        PauseMenuCreated.transform.SetParent(_canvas.transform, false);
        PauseMenuCreated.GetComponent<RectTransform>().localPosition =
            _pauseMenu.GetComponent<RectTransform>().localPosition;
        PauseMenuCreated.Container(_translator, _pauseService);
    }

    private void CreateGame()
    {
        _entryPoint.Container(Container.Resolve<DiContainer>());
        _entryPoint.CreateGame();
    }
}
