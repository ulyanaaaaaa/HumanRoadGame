using System;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public Action OnPause;

    public void Click()
    {
        Time.timeScale = 0;
        OnPause?.Invoke();
    }
}
