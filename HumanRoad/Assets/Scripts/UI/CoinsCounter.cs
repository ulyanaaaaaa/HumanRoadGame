using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinsCounter : MonoBehaviour
{
    private TextMeshProUGUI _counter;
    private Player _player;

    public void Setup(Player player)
    {
        _player = player;
    }

    private void Awake()
    {
        _counter = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _player.GetComponent<PlayerWallet>().OnChangeCoins += (coins => 
            ChangedCounter(_player.GetComponent<PlayerWallet>().PlayerWalletCoins()));
    }

    private void ChangedCounter(int value)
    {
        _counter.text = (value + 1).ToString();
    }
}
