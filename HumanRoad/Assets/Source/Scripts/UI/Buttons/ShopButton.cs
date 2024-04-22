using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(TextTranslator))]
public class ShopButton : MonoBehaviour
{
    public Action OnPlay;
    
    private void Awake()
    {
        GetComponent<TextTranslator>().Id = "shop";
    }
    
    public void Click()
    {
        OnPlay?.Invoke();
    }
}
