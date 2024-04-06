using System;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public Action OnExit;

    public void Click()
    {
        OnExit?.Invoke();
    }
}
