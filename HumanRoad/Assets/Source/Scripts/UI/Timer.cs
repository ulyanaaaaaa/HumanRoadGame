using System;
using System.Collections;
using UnityEngine;
using Zenject;

public class Timer : MonoBehaviour
{
    public Action<float,float> OnDurationChanged;
    public bool IsTook { get; private set; }

    [SerializeField] private float _startDuration;
    private float _duration;
    private Coroutine _timerTick;
    private Player _player;
    [SerializeField] private GameInstaller _gameInstaller;

    [Inject]
    public void Container(Player player, GameInstaller gameInstaller)
    {
        _player = player;
        _gameInstaller = gameInstaller;
    }
    
    private void Start()
    {
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += StartTimer;
    }

    private void StartTimer()
    {
        _duration = _startDuration;
        _timerTick = StartCoroutine(TimerTick());
        _gameInstaller.PlayerCreated.OnDie += () => StopCoroutine(_timerTick);
    }

    public IEnumerator StopTimerCoroutine(float time)
    {
        IsTook = true;
        StopCoroutine(_timerTick);
        yield return new WaitForSeconds(time);
        IsTook = false;
        _timerTick = StartCoroutine(TimerTick());
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
