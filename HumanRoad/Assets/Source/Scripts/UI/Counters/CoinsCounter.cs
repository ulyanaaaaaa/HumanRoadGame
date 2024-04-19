using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinsCounter : MonoBehaviour
{
    private TextMeshProUGUI _counter;
    private GameInstaller _gameInstaller;

    [Inject]
    public void Constructor(GameInstaller installer)
    {
        _gameInstaller = installer;
    }

    private void Awake()
    {
        _counter = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _gameInstaller.PlayerCreated.GetComponent<PlayerWallet>().OnChangeCoins += (coins => 
            ChangedCounter(_gameInstaller.PlayerCreated.GetComponent<PlayerWallet>().PlayerWalletCoins()));
        _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += Reset;
    }

    private void Reset()
    {
        ChangedCounter(-1);
    }

    private void ChangedCounter(int value)
    {
        _counter.text = (value + 1).ToString();
    }
}
