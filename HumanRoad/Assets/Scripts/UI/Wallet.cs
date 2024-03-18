using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Wallet : MonoBehaviour
{
    private TextMeshProUGUI _counter;
    public int CoinsCount { get; private set; }
    
    private void Awake()
    {
        _counter = GetComponent<TextMeshProUGUI>();
        
        if (PlayerPrefs.HasKey("Wallet"))
            CoinsCount = PlayerPrefs.GetInt("Wallet");
        
        if (PlayerPrefs.HasKey("Coins"))
            CoinsCount += PlayerPrefs.GetInt("Coins");
        
        UpdateCounter();
    }
    
    public bool TrySpend(int value)
    {
        if (value < CoinsCount)
        {
            RemoveCoins(value);
            return true;
        }
        else
        {
            throw new Exception("Not enough coins!");
            return false;
        }
    }

    private void RemoveCoins(int value)
    {
        CoinsCount -= value;
        UpdateCounter();
    }
    
    private void AddCoins(int value)
    {
        CoinsCount += value;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        _counter.text = "Wallet: " + CoinsCount;
        PlayerPrefs.SetInt("Wallet", CoinsCount);
    }
}
