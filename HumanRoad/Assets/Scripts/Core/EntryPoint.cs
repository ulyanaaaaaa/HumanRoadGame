using System.Collections.Generic;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    [SerializeField] public List<Hurt> Hurts;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private Vector3 _playerStartPosition;

    private Menu _menu;
    private Menu _menuCreated;
    private Hurt _hurt;
    private Hurt _hurtCreated;
    private LooseHurt _looseHurt;
    private LooseHurt _looseHurtCreated;
    private Player _player;
    private Player _playerCreated;
    private TerrainSpawner _terrainSpawner;
    private ScoreCounter _scoreCounter;
    private ScoreCounter _scoreCounterCreated;

    private void Awake()
    {
        _menu = Resources.Load<Menu>("Menu");
        _looseHurt = Resources.Load<LooseHurt>("LooseHurt");
        _hurt = Resources.Load<Hurt>("Hurt");
        _scoreCounter = Resources.Load<ScoreCounter>("ScoreCounter");
        _terrainSpawner = GetComponent<TerrainSpawner>();
        _player = Resources.Load<Player>("Player");
        CreateMenu();
    }

    private void CreatePlayer()
    {
        _playerCreated = Instantiate(_player, _playerStartPosition, Quaternion.Euler(0,90,0));
        _terrainSpawner.Setup(_playerCreated.GetComponent<KeyboardInput>());
        _playerCreated.OnLooseHurt += CreateLooseHurt;
        _playerCreated.IsDie += CreateMenu;
        _playerCreated.IsDie += RestartLevel;
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

    private void CreateHurts()
    {
        _hurtCreated = Instantiate(_hurt,
            _hurt.GetComponent<RectTransform>().localPosition,
            Quaternion.identity,
            _canvas.transform);
        _hurtCreated.GetComponent<RectTransform>().localPosition =
            _hurt.GetComponent<RectTransform>().localPosition;
        Hurts.Add(_hurtCreated);
        
        _hurtCreated = Instantiate(_hurt,
            _hurt.GetComponent<RectTransform>().localPosition + new Vector3(135f, 0, 0),
            Quaternion.identity,
            _canvas.transform);
        _hurtCreated.GetComponent<RectTransform>().localPosition = 
            _hurt.GetComponent<RectTransform>().localPosition + new Vector3(135f, 0, 0);
        Hurts.Add(_hurtCreated);
        
        _hurtCreated = Instantiate(_hurt,
            _hurt.GetComponent<RectTransform>().localPosition + new Vector3(270f, 0, 0),
            Quaternion.identity,
            _canvas.transform);
        _hurtCreated.GetComponent<RectTransform>().localPosition = 
            _hurt.GetComponent<RectTransform>().localPosition + new Vector3(270f, 0, 0);
        Hurts.Add(_hurtCreated);
    }

    private void CreateLooseHurt()
    {
        _looseHurtCreated = Instantiate(_looseHurt,
            Hurts[0].GetComponent<RectTransform>().localPosition,
            Quaternion.identity,
            _canvas.transform);
        _looseHurtCreated.GetComponent<RectTransform>().localPosition =
            Hurts[0].GetComponent<RectTransform>().localPosition;
        Hurts.RemoveAt(0);
    }

    private void CreateMenu()
    {
        _menuCreated = Instantiate(_menu,
            _menu.GetComponent<RectTransform>().localPosition,
            Quaternion.identity,
            _canvas.transform);
        _menuCreated.GetComponent<RectTransform>().localPosition =
            _menu.GetComponent<RectTransform>().localPosition;
        _menuCreated.GetComponentInChildren<PlayButton>().OnPlay += CreatePlayer;
        _menuCreated.GetComponentInChildren<PlayButton>().OnPlay += CreateScoreCounter;
        _menuCreated.GetComponentInChildren<PlayButton>().OnPlay += CreateHurts;
        _menuCreated.GetComponentInChildren<PlayButton>().OnPlay += DisableMenu;
    }

    private void DisableMenu()
    {
        Destroy(_menuCreated.gameObject);
    }

    private void RestartLevel()
    {
        Destroy(_scoreCounterCreated.gameObject);
        Destroy(_hurtCreated.gameObject);
        Destroy(_looseHurtCreated.gameObject);
        Destroy(_playerCreated.gameObject);
    }
}
