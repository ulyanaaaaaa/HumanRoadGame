using System;
using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(TextTranslator))]
public class Wallet : MonoBehaviour, ISaveData
{
    public string Id { get; set; }
    
    [SerializeField]private TextMeshProUGUI _counter;
    [SerializeField] private SaveService _saveService;
    [SerializeField] private TextTranslator _textTranslator;
    [SerializeField] private GameInstaller _gameInstaller;
    [field: SerializeField] public int CoinsCount { get; private set; }

    [Inject]
    public void Container(SaveService saveService, GameInstaller installer)
    {
        _saveService = saveService;
        _gameInstaller = installer;
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
            CoinsCount = walletSaveData.CoinsCount +  PlayerPrefs.GetInt("Coins");
            Debug.Log("Загруженные монеты из уровня  " + PlayerPrefs.GetInt("Coins"));
            Debug.Log("Загруженные монеты из сохранения  " + walletSaveData.CoinsCount);
            UpdateCounter();
        }
        Save();
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
        Load();
        
        _textTranslator.TranslateText += UpdateCounter;
            _gameInstaller.MenuCreated.GetComponentInChildren<PlayButton>().OnPlay += Save;
        _gameInstaller.PlayerCreated.OnDie += Load;
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
        Debug.Log("WalletSaveData      " + CoinsCount);
    }
}
