using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(TextTranslator))]
public class PlayButton : MonoBehaviour
{
    public Action OnPlay;

    private void Awake()
    {
        GetComponent<TextTranslator>().Id = "main_play";
    }
    
    public void Click()
    {
        OnPlay?.Invoke();
    }
}
