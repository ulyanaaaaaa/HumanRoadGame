using System;
using UnityEngine;
using Zenject;

public class PauseMenu : MonoBehaviour
{
    public Action OnPlay;
    public Translator Translator;

    [Inject]
    public void Container(Translator translator)
    {
        Translator = translator;
    }

    private void Start()
    {
        GetComponentInChildren<TextTranslator>().Id = "go";
    }

    public void Click()
    {
        Time.timeScale = 1;
        OnPlay?.Invoke();
    }
}
