using System;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(TextTranslator))]
public class Wallet : MonoBehaviour
{
    public string Id { get; set; } = "wallet";
    
    private TextMeshProUGUI _counter;
    private TextTranslator _textTranslator;
    private GameInstaller _gameInstaller;
    private ISaveService _saveService;
    [field: SerializeField] public int CoinsCount { get; private set; }

    [Inject]
    public void Constructor(GameInstaller gameInstaller)
    {
        _gameInstaller = gameInstaller;
    }
    
    private void Awake()
    {
        _textTranslator = GetComponent<TextTranslator>();
        _counter = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        _saveService = new SaveService();
        _gameInstaller.PlayerCreated.OnDie += AddPlayerCoins;
        _textTranslator.TranslateText += UpdateCounter;
        
        _textTranslator.Id = Id;

        if (!_saveService.Exists(Id))
        {
            CoinsCount = 0;
            Save();
        }
        else
        {
            Load();
        }

        UpdateCounter();
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

    private void AddPlayerCoins()
    {
        CoinsCount += PlayerPrefs.GetInt("Coins");
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        _counter.text = _textTranslator.Translate(Id) + '\n'+ CoinsCount; 
        Save();
    }

    private void Save()
    {
        WalletSaveData e = new WalletSaveData();
        e.CoinsCount = CoinsCount;
        _saveService.Save(Id, e);
    }

    private void Load()
    {
        _saveService.Load<WalletSaveData>(Id, e =>
        {
            CoinsCount = e.CoinsCount;
        });
    }
}

public class WalletSaveData
{
    [JsonProperty(PropertyName = "count")]
    public int CoinsCount { get; set; }
}
