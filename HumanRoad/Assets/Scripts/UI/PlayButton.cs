using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class PlayButton : MonoBehaviour
{
    public Action OnPlay;

    public void OnClick()
    {
        OnPlay?.Invoke();
    }
}
