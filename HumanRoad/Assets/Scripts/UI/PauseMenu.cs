using System;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private Button _button;
    public Action OnPlay;

    private void Start()
    {
        _button = GetComponentInChildren<Button>();
    }

    public void OnClick()
    {
        Time.timeScale = 1;
        OnPlay?.Invoke();
    }
}
