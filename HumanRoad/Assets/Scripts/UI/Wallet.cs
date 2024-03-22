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
        _saveService.SaveData.AddData(Id, new WalletSaveData(Id, typeof(Wallet), CoinsCount));
        Debug.Log("SaveWallet " + CoinsCount);
    }

    public void Load()
    {
        Debug.Log("LoadWallet");
        if (_saveService.SaveData.TryGetData(Id, out WalletSaveData walletSaveData))
        {
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" + walletSaveData.CoinsCount);
            CoinsCount = walletSaveData.CoinsCount;
            Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA" + CoinsCount);
        }
    }
    
    private void Start()
    {
        Id = "Wallet";
        _counter = GetComponent<TextMeshProUGUI>();
        
        Load();
        
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
    }
}
