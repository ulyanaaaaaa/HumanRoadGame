using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(TextTranslator))]
public class Wallet : MonoBehaviour, ISaveData
{
    public string Id { get; set; }
    
    private TextMeshProUGUI _counter;
    private SaveService _saveService;
    private TextTranslator _textTranslator;
    [field: SerializeField] public int CoinsCount { get; private set; }

    public void Setup(SaveService saveService)
    {
        _saveService = saveService;
    }
    
    public void Save()
    {
        _saveService.SaveData.AddData(Id, new WalletSaveData(Id, typeof(Wallet), CoinsCount)); //не сохраняет
        Debug.Log("Cохраненные монеты                 " + CoinsCount);
        _saveService.Save();
    }

    public void Load()
    {  
        _saveService.Load();
        if (_saveService.SaveData.TryGetData(Id, out WalletSaveData walletSaveData))
        {
            CoinsCount = walletSaveData.CoinsCount + 1 + PlayerPrefs.GetInt("Coins");
            Debug.Log("Загруженные монеты из уровня  " + PlayerPrefs.GetInt("Coins"));
            Debug.Log("Загруженные монеты из сохранения  " + walletSaveData.CoinsCount);
        }
      
    }
    
    private void Awake()
    {
        _textTranslator = GetComponent<TextTranslator>();
        _textTranslator.TranslateText += UpdateCounter;
    }

    private void Start()
    {
        Id = "wallet";
        GetComponent<TextTranslator>().Id = Id;
        Load();
        
        _counter = GetComponent<TextMeshProUGUI>();
        
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

    private void UpdateCounter()
    {
        _counter.text = _textTranslator.Translate(Id) + '\n'+ CoinsCount;  
        Save();
    }
}

[Serializable]
public class WalletSaveData : SaveData
{
    public int CoinsCount;
    
    public WalletSaveData(string id, Type type, int coinsCount) : base(id, type)
    {
        CoinsCount = coinsCount;
        Debug.Log("WalletSaveData      "+CoinsCount);
    }
}
