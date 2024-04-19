using System;
using UnityEngine;
using Zenject;

public class PauseMenu : MonoBehaviour
{
    public Action OnPlay;
    public Translator Translator;
    [SerializeField] private PauseService _pauseService;

    [Inject]
    public void Container(Translator translator, PauseService pauseService)
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
