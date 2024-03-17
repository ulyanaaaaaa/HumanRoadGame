using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopItem : MonoBehaviour
{
    [field: SerializeField] public int Price { get; private set; }
    
    private Wallet _wallet;
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(TryBuy);
    }
    
    private void TryBuy()
    {
        if (_wallet.TrySpend(Price)) {
            //изменение скина Player
        }
    }
}
