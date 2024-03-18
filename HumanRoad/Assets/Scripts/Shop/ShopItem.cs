using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopItem : MonoBehaviour
{
    [field: SerializeField] public int Price { get; private set; }
    [SerializeField] private TextMeshProUGUI _skinName;
    private Wallet _wallet;
    private Shop _shop;
    private Button _button;

    private void Start()
    {
        _shop = GetComponentInParent<Shop>();
        _wallet = _shop.Wallet;
        _button = GetComponent<Button>();
        _button.onClick.AddListener(TryBuy);
    }
    
    private void TryBuy()
    {
        if (_wallet.TrySpend(Price)) {
            Debug.Log("Buy");
            PlayerPrefs.SetString("Skin", _skinName.ToString());
        }
    }
}
