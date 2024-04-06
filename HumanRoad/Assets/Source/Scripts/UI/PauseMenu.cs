using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public Action OnPlay;
    public Translator Translator;

    public void Setup(Translator translator)
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
