using System;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    public Action OnPlay;

    public void OnClick()
    {
        OnPlay?.Invoke();
    }
}
