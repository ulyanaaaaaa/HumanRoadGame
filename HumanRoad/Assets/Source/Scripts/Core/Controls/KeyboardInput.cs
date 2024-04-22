using System;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    public Action OnRightCliked;
    public Action OnLeftCliked;
    public Action OnBackCliKed;
    public Action OnRunCliked;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
            OnRunCliked?.Invoke();

        if (Input.GetKeyDown(KeyCode.D))
            OnRightCliked?.Invoke();

        if (Input.GetKeyDown(KeyCode.A))
            OnLeftCliked?.Invoke();

        if (Input.GetKeyDown(KeyCode.S))
            OnBackCliKed?.Invoke();
    }
}
