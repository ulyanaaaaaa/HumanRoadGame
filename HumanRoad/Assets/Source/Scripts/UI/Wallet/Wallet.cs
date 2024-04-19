using System;
using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(TextTranslator))]
public class Wallet : MonoBehaviour
{
    public string Id { get; set; }
    
    [SerializeField]private TextMeshProUGUI _counter;
    [SerializeField] private TextTranslator _textTranslator;
    [SerializeField] private GameInstaller _gameInstaller;
    [field: SerializeField] public int CoinsCount { get; private set; }

    [Inject]
    public void Container(GameInstaller installer)
    {
        _gameInstaller = installer;
    }
    
    private void Awake()
    {
        _textTranslator = GetComponent<TextTranslator>();
        _counter = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        Id = "wallet";
        _textTranslator.Id = Id;
        
        _textTranslator.TranslateText += UpdateCounter;
    }
    
    public bool TrySpend(int value)
    {
        if (value <= CoinsCount)
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
        _counter.text = _textTranslator.Translate(Id) + '\n'+ CoinsCount; 
    }
}

