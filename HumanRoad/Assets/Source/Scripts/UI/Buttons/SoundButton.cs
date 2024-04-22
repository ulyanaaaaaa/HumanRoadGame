using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(TextTranslator))]
public class SoundButton : MonoBehaviour
{
    public Action OnPlay;
    
    private void Awake()
    {
        GetComponent<TextTranslator>().Id = "sound_button";
    }
    
    public void Click()
    {
        OnPlay?.Invoke();
    }
}
