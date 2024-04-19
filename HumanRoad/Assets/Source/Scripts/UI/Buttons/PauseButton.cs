using System;
using UnityEngine;
using Zenject;

public class PauseButton : MonoBehaviour
{
    public Action OnPause;
    private PauseService _pauseService;

    [Inject]
    public void Constructor(PauseService pauseService)
    {
        _pauseService = pauseService;
    }

    public void Click()
    {
        OnPause?.Invoke();
        _pauseService.Pause();
    }
}
