using System;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Wallet : MonoBehaviour, ISaveData
{
    public string Id { get; set; }
    
    private TextMeshProUGUI _counter;
    private SaveService _saveService;
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

    private void Start()
    {
        Id = "Wallet";
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
        _counter.text = "Wallet: " + CoinsCount;
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
