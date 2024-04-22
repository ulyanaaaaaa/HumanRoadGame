using System;
using UnityEngine;
using Zenject;

public class PauseMenu : MonoBehaviour
{
    public Action OnPlay;
    public Translator Translator;
    private PauseService _pauseService;

    [Inject]
    public void Constructor(Translator translator, PauseService pauseService)
    {
        Translator = translator;
        _pauseService = pauseService;
    }

    private void Start()
    {
        GetComponentInChildren<TextTranslator>().Id = "go";
    }

    public void Click()
    {
        OnPlay?.Invoke();
        _pauseService.Resume();
    }
}
