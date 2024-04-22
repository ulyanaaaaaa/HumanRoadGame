using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(ShopItem))]
public class ShopItemViewer : MonoBehaviour
{
    public Action OnUpdatePrice;
    
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private TextTranslator _textTranslator;
    private ShopItem _shopItem;

    private void Awake()
    {
        _shopItem = GetComponent<ShopItem>();
    }

    private void OnEnable()
    {
        _textTranslator.TranslateText += UpdatePrice;
        OnUpdatePrice += UpdatePrice;
    }

    private void UpdatePrice()
    {
        _text.text = _textTranslator.Translate(_textTranslator.Id) + '\n' + _shopItem.Price;
    }
}
