using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShopItemViewer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    private Wallet _wallet;
    private Image _background;
    private ShopItem _shopItem;
    private Shop _shop;
    
    private void Start()
    { 
        _shopItem = GetComponent<ShopItem>();
        _shop = GetComponentInParent<Shop>();
        _wallet = _shop.Wallet;
        _background = GetComponent<Image>();
        _text.text = "Price: " + _shopItem.Price;
    }

    private void Update()
    {
        if (_shopItem.Price > _wallet.CoinsCount)
        {
            _background.color = new Color(255, 255, 255, 255);
        }
        else
        {
            _background.color = new Color(75, 75, 75, 255);
        }
    }
}
