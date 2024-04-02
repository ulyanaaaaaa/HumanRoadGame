using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(TextTranslator))]
public class LanguageButton : MonoBehaviour
{
    public Action OnClick;

    public void Click()
    {
        OnClick?.Invoke();
    }
    
    private void Awake()
    {
        GetComponent<TextTranslator>().SetId("language_button");
    }
}
