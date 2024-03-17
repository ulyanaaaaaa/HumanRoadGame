using System.ComponentModel;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Wallet : MonoBehaviour
{
    private TextMeshProUGUI _counter;
    private int _coinsCount;
    
    private void Awake()
    {
        _counter = GetComponent<TextMeshProUGUI>();
        
        if (PlayerPrefs.HasKey("Wallet"))
            _coinsCount = PlayerPrefs.GetInt("Wallet");
        
        if (PlayerPrefs.HasKey("Coins"))
            _coinsCount += PlayerPrefs.GetInt("Coins");
        
        UpdateCounter();
    }
    
    public bool TrySpend(int value)
    {
        if (value < _coinsCount)
        {
            RemoveCoins(value);
            return true;
        }
        else
        {
            throw new WarningException("Not enough coins!");
            return false;
        }
    }

    private void RemoveCoins(int value)
    {
        _coinsCount -= value;
        UpdateCounter();
    }
    
    private void AddCoins(int value)
    {
        _coinsCount += value;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        _counter.text = "Wallet: " + _coinsCount;
        PlayerPrefs.SetInt("Wallet", _coinsCount);
    }
}
