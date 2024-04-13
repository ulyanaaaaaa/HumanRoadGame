using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopItem : MonoBehaviour
{
    [field: SerializeField] public int Price { get; private set; }
    [SerializeField] private TextMeshProUGUI _skinName;
    [SerializeField] private TextTranslator _textTranslator;
    private Wallet _wallet;
    private ShopItemViewer _shopItemViewer;
    private Shop _shop;
    private Button _button;
    private SaveService _saveService;
    private Player _player;

    public void Save()
    {
        //Debug.Log("ShopItem: " + _skinName.text);
        //_saveService.SaveData.AddData("Player", new PlayerSaveData("Player", typeof(Player), _skinName.text));
    }

    private void Start()
    {
        _shop = GetComponentInParent<Shop>();
        _shopItemViewer = GetComponent<ShopItemViewer>();
        _player = _shop.Player;
        _wallet = _shop.Wallet;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(TryBuy);
        _shopItemViewer.OnUpdatePrice?.Invoke();
        _saveService = _shop.SaveService;
    }
    
    private void TryBuy()
    {
        if (_wallet.TrySpend(Price))
        {
            Price = 0;
            _player.ChangeSkin(_textTranslator.Id);
            _shopItemViewer.OnUpdatePrice?.Invoke();
            Save();
        }
    }
}
