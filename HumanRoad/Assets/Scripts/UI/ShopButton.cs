using System;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    public Action OnPlay;

    public void OnClick()
    {
        OnPlay?.Invoke();
    }
}
