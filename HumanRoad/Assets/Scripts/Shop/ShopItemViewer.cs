using UnityEngine;
using UnityEngine.UI;

public class ShopItemViewer : MonoBehaviour
{
    [SerializeField] private Wallet _wallet;
    [SerializeField] private Image _background;

    private ShopItem _shopItem;

    private void Awake()
    {
        _shopItem = GetComponent<ShopItem>();
    }
    
    
}
