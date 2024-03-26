using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Action OnDie;
    public Action<float,float> OnDurationChanged;
    public bool IsTook { get; private set; }

    [SerializeField] private float _duration;
    private float _startDuration;
    private Coroutine _timerTick;
    private Player _player;

    public void Setup(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        _startDuration = _duration;
        _timerTick = StartCoroutine(TimerTick());
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
