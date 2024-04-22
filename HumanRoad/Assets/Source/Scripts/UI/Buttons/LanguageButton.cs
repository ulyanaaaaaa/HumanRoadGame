using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(TextTranslator))]
public class LanguageButton : MonoBehaviour
{
    public Action OnClick;
    
    private void Awake()
    {
        GetComponent<TextTranslator>().Id = "language_button";
    }
    
    public void Click()
    {
        OnClick?.Invoke();
    }
}
