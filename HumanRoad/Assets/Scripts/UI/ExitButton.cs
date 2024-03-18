using System;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public Action OnExit;

    public void OnClick()
    {
        OnExit?.Invoke();
    }
}
