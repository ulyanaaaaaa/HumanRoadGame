using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopItem : MonoBehaviour
{
    [field: SerializeField] public int Price { get; private set; }
    [SerializeField] private TextMeshProUGUI _skinName;
    private Wallet _wallet;
    private ShopItemViewer _shopItemViewer;
    private Shop _shop;
    private Button _button;

    private void Start()
    {
        _shop = GetComponentInParent<Shop>();
        _shopItemViewer = GetComponent<ShopItemViewer>();
        _wallet = _shop.Wallet;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(TryBuy);
        _shopItemViewer.OnUpdatePrice?.Invoke();
    }
    
    private void TryBuy()
    {
        if (_wallet.TrySpend(Price))
        {
            Price = 0;
            _shopItemViewer.OnUpdatePrice?.Invoke();
            PlayerPrefs.SetString("Skin", _skinName.text);
        }
    }
}
