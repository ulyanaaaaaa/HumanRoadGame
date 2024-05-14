using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Timer : MonoBehaviour, IPause
{
    public Action<float,float> OnDurationChanged;
    public bool IsTook { get; private set; }

    [SerializeField] private float _startDuration;
    private float _duration;
    private Coroutine _timerTick;
    private Coroutine _stopTimerTick;
    private Player _player;
    private PauseService _pauseService;
    private GameInstaller _gameInstaller;

    [Inject]
    public void Constructor(Player player, GameInstaller gameInstaller, PauseService pauseService)
    {
        _pauseService = pauseService;
        _player = player;
        _gameInstaller = gameInstaller;
    }
    
    private void Start()
    {
        _pauseService.AddPause(this);
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += StartTimer;
    }
    
    public void Pause()
    {
        StopCoroutine(_timerTick);
    }

    public void Resume()
    {
        _timerTick = StartCoroutine(TimerTick());
    }

    public void StopTimer(float time)
    {
        if(_stopTimerTick != null)
            StopCoroutine(_stopTimerTick);
        
        _stopTimerTick = StartCoroutine(StopTimerCoroutine(time));
    }
    
    private IEnumerator StopTimerCoroutine(float time)
    {
        IsTook = true;
        Debug.Log("Stop");
        StopCoroutine(_timerTick);
        yield return new WaitForSeconds(time);
        IsTook = false;
        _timerTick = StartCoroutine(TimerTick());
        Debug.Log("Start");
    }

    private void StartTimer()
    {
        Debug.Log("Start");
        _duration = _startDuration;
        gameObject.SetActive(true); 
        _timerTick = StartCoroutine(TimerTick());
        _gameInstaller.PlayerCreated.OnDie += () =>
        {
            StopCoroutine(_timerTick);
            gameObject.SetActive(false); 
        };
    }

    private IEnumerator TimerTick()
    {
        while (_duration > 0)
        {
            yield return new WaitForSeconds(0.1f);
            _duration -= 0.1f;
            OnDurationChanged?.Invoke(_duration, _startDuration);
        }
        _player.OnDie?.Invoke();
    }
}
