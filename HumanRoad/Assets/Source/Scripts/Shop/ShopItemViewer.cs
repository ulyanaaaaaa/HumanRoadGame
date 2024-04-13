using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShopItemViewer : MonoBehaviour
{
    public Action OnUpdatePrice;
    
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextTranslator _textTranslator;
    private Wallet _wallet;
    private Image _background;
    private ShopItem _shopItem;

    private void Start()
    {
        _wallet = GetComponentInParent<Shop>().Wallet;
        _shopItem = GetComponent<ShopItem>();
        _background = GetComponent<Image>();
    }

    private void OnEnable()
    {
        OnUpdatePrice += UpdatePrice;
    }

    private void UpdatePrice()
    {
        _text.text = _textTranslator.Translate(_textTranslator.Id) + '\t' + _shopItem.Price;
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
