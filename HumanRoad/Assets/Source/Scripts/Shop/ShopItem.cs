using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopItem : MonoBehaviour
{
    [field: SerializeField] public int Price { get; private set; }
    [SerializeField] private TextMeshProUGUI _skinName;
    [SerializeField] private TextTranslator _textTranslator;
    [SerializeField] private string _id;
    private Wallet _wallet;
    private ShopItemViewer _shopItemViewer;
    private Shop _shop;
    private Button _button;
    private Player _player;
    private ISaveService _saveService;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _shop = GetComponentInParent<Shop>();
        _shopItemViewer = GetComponent<ShopItemViewer>();
    }

    private void Start()
    {
        _saveService = new SaveService();
        _player = _shop.Player;
        _wallet = _shop.Wallet;
        _button.onClick.AddListener(TryBuy);
        
        if (!_saveService.Exists(_id))
            Save(); 
        else
            Load(); 
        
        _shopItemViewer.OnUpdatePrice?.Invoke();
    }
    
    private void TryBuy()
    {
        if (_wallet.TrySpend(Price))
        {
            Price = 0;
            Save();
            _player.ChangeSkin(_textTranslator.Id);
            _shopItemViewer.OnUpdatePrice?.Invoke();
        }
    }
    
    private void Save()
    {
        ShopItemSaveData e = new ShopItemSaveData();
        e.Price = Price;
        _saveService.Save(_id, e);
    }

    private void Load()
    {
        _saveService.Load<ShopItemSaveData>(_id, e =>
        {
            Price = e.Price;
        });
    }
}

public class ShopItemSaveData
{
    public int Price { get; set; }
}
