using System;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public Action OnPause;

    public void OnClick()
    {
        Time.timeScale = 0;
        OnPause?.Invoke();
    }
}
